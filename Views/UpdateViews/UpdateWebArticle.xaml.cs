using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RefCatalogue.Controllers;

namespace RefCatalogue.Views.UpdateViews
{
    /// <summary>
    /// Interaction logic for UpdateWebArticle.xaml
    /// </summary>
    public partial class UpdateWebArticle : Page
    {
        private readonly DataProcessor _dataProcessor = new();
        private DataRow _refToUpdate;
        private const string Task = "dbo.Update_Website";

        public UpdateWebArticle(DataRow selectedRow)
        {
            InitializeComponent();
            this._refToUpdate = selectedRow;
            LoadTextboxData(selectedRow);
        }

        private void LoadTextboxData(DataRow data)
        {
            if (data.Field<string>("RefType") == "Website")
            {
                var author1Surname = !string.IsNullOrEmpty(data.Field<string>("Author1FN")) ? web1surname : organisation;

                webArticleTitle.Text = data.Field<string>("WebpageTitle") ?? string.Empty;
                web1first.Text = data.Field<string>("Author1FN") ?? string.Empty;
                author1Surname.Text = data.Field<string>("Author1SN") ?? string.Empty;
                web2first.Text = data.Field<string>("Author2FN") ?? string.Empty;
                web2surname.Text = data.Field<string>("Author2SN") ?? string.Empty;
                web3first.Text = data.Field<string>("Author3FN") ?? string.Empty;
                web3surname.Text = data.Field<string>("Author3SN") ?? string.Empty;
                webpageYear.Text = data.Field<int>("Year").ToString();
                webURL.Text = data.Field<string>("WebURL") ?? string.Empty;
                accessDate.SelectedDateTime = DateTime.Parse(data.Field<string>("accessDate") ?? string.Empty);
            }
            else
            {
                webArticleTitle.Text = data.Field<string>("WebpageTitle") ?? string.Empty;
                web1first.Text = data.Field<string>("Author1FN") ?? string.Empty;
                web1surname.Text = data.Field<string>("Author1SN") ?? string.Empty;
                web2first.Text = data.Field<string>("Author2FN") ?? string.Empty;
                web2surname.Text = data.Field<string>("Author2SN") ?? string.Empty;
                web3first.Text = data.Field<string>("Author3FN") ?? string.Empty;
                web3surname.Text = data.Field<string>("Author3SN") ?? string.Empty;
                webpageYear.Text = data.Field<int>("Year").ToString();
                webURL.Text = data.Field<string>("WebURL") ?? string.Empty;
                accessDate.SelectedDateTime = DateTime.Parse(data.Field<string>("accessDate") ?? string.Empty);
                blogSiteTitle.Text = data.Field<string>("blogSiteTitle") ?? string.Empty;
                postedDate.SelectedDateTime = DateTime.Parse(data.Field<string>("postedDate") ?? string.Empty);
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(blogSiteTitle.Text))
            {
                var accessedDate = (DateTime)accessDate.SelectedDateTime;
                var author1Surname = string.IsNullOrEmpty(organisation.Text) ? web1surname : organisation;

                var requiredTextboxes = new[]
                {
                    webArticleTitle,
                    web1first,
                    author1Surname,
                    webpageYear,
                    webURL
                };

                var errors = FormHelper.ValidateTextboxes(requiredTextboxes);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join(Environment.NewLine, errors), "Missing Required Fields", MessageBoxButton.OK);
                    return;
                }
                var websiteDetails = new Dictionary<string, string>
                {
                    { "id", _refToUpdate.Field<int>("Id").ToString() },
                    { "webpagetitle", webArticleTitle.Text },
                    { "web1first", web1first.Text },
                    { "web1last", author1Surname.Text },
                    { "web2first", web2first.Text },
                    { "web2last", web2surname.Text },
                    { "web3first", web3first.Text },
                    { "web3last", web3surname.Text },
                    { "year", webpageYear.Text },
                    { "webURL", webURL.Text },
                    { "accessDate", accessedDate.ToString("dd MMMM yyyy")},
                };

                // Run update script
                var result = _dataProcessor.PushWebsiteDetailsToDatabase(websiteDetails, Task);
                if (result == 1)
                {
                    MessageBox.Show($"{webArticleTitle.Text} successfully updated.", "Update Website", MessageBoxButton.OK);
                    var updateRef = new UpdateRefPage(new DataProcessor());
                    NavigationService?.Navigate(updateRef);
                }
            }
            else
            {
                
                var accessedDate = (DateTime)accessDate.SelectedDateTime;
                var postDate = (DateTime)postedDate.SelectedDateTime;

                var requiredTextboxes = new[]
                {
                    webArticleTitle,
                    web1first,
                    web1surname,
                    webpageYear,
                    webURL,
                    blogSiteTitle
                };

                var errors = FormHelper.ValidateTextboxes(requiredTextboxes);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join(Environment.NewLine, errors), "Missing Required Fields", MessageBoxButton.OK);
                    return;
                }
                var blogDetails = new Dictionary<string, string>
                {
                    { "id", _refToUpdate.Field<int>("Id").ToString() },
                    { "articleTitle", webArticleTitle.Text },
                    { "web1first", web1first.Text },
                    { "web1last", web1surname.Text },
                    { "web2first", web2first.Text },
                    { "web2last", web2surname.Text },
                    { "web3first", web3first.Text },
                    { "web3last", web3surname.Text },
                    { "year", webpageYear.Text },
                    { "webURL", webURL.Text },
                    { "accessDate", accessedDate.ToString("dd MMMM yyyy")},
                    { "websiteName", blogSiteTitle.Text},
                    { "postedDate", postDate.ToString("dd MMMM")}
                };

                // Run update script
                var result = _dataProcessor.PushBlogDetailsToDatabase(blogDetails, Task);
                if (result == 1)
                {
                    MessageBox.Show($"{webArticleTitle.Text} successfully updated.", "Update Blog", MessageBoxButton.OK);
                    NavigationService?.GoBack();
                }
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}