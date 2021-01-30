using RefCatalogue.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RefCatalogue.Views
{
    /// <summary>
    /// Interaction logic for UpdateWebArticle.xaml
    /// </summary>
    public partial class UpdateWebArticle : Page
    {
        private readonly DataProcessor dataProcessor = new DataProcessor();
        private DataRow refToUpdate;

        public UpdateWebArticle(DataRow refToUpdate)
        {
            InitializeComponent();
            this.refToUpdate = refToUpdate;
            LoadTextboxData(refToUpdate);
        }

        private void LoadTextboxData(DataRow refToUpdate)
        {
            if (refToUpdate.Field<string>("RefType") == "Website")
            {
                TextBox Author1Surname = string.IsNullOrEmpty(refToUpdate.Field<string>("Author1SN")) ? web1surname : organisation;

                webArticleTitle.Text = refToUpdate.Field<string>("WebpageTitle");
                web1first.Text = refToUpdate.Field<string>("Author1FN");
                Author1Surname.Text = refToUpdate.Field<string>("Author1SN");
                web2first.Text = refToUpdate.Field<string>("Author2FN");
                web2surname.Text = refToUpdate.Field<string>("Author2SN");
                web3first.Text = refToUpdate.Field<string>("Author3FN");
                web3surname.Text = refToUpdate.Field<string>("Author3SN");
                webpageYear.Text = refToUpdate.Field<int>("Year").ToString();
                webURL.Text = refToUpdate.Field<string>("WebURL");
                accessDate.SelectedDateTime = refToUpdate.Field<DateTime>("accessDate");
            }
            else
            {
                webArticleTitle.Text = refToUpdate.Field<string>("WebpageTitle");
                web1first.Text = refToUpdate.Field<string>("Author1FN");
                web1surname.Text = refToUpdate.Field<string>("Author1SN");
                web2first.Text = refToUpdate.Field<string>("Author2FN");
                web2surname.Text = refToUpdate.Field<string>("Author2SN");
                web3first.Text = refToUpdate.Field<string>("Author3FN");
                web3surname.Text = refToUpdate.Field<string>("Author3SN");
                webpageYear.Text = refToUpdate.Field<int>("Year").ToString();
                webURL.Text = refToUpdate.Field<string>("WebURL");
                accessDate.SelectedDateTime = refToUpdate.Field<DateTime>("accessDate");
                blogSiteTitle.Text = refToUpdate.Field<string>("blogSiteTitle");
                postedDate.SelectedDateTime = refToUpdate.Field<DateTime>("postedDate");
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(blogSiteTitle.Text))
            {
                string task = "dbo.Update_Website";
                DateTime accessedDate = (DateTime)accessDate.SelectedDateTime;
                TextBox Author1Surname = string.IsNullOrEmpty(organisation.Text) ? web1surname : organisation;

                TextBox[] requiredTextboxes = new TextBox[]
                {
                    webArticleTitle,
                    web1first,
                    Author1Surname,
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
                    { "id", refToUpdate.Field<int>("Id").ToString() },
                    { "webpagetitle", webArticleTitle.Text },
                    { "web1first", web1first.Text },
                    { "web1last", Author1Surname.Text },
                    { "web2first", web2first.Text },
                    { "web2last", web2surname.Text },
                    { "web3first", web3first.Text },
                    { "web3last", web3surname.Text },
                    { "year", webpageYear.Text },
                    { "webURL", webURL.Text },
                    { "accessDate", accessedDate.ToString("dd MMMM yyyy")},
                };

                // Run update script
                var result = dataProcessor.PushWebsiteDetailsToDatabase(websiteDetails, task);
                if (result == 1)
                {
                    MessageBox.Show($"{webArticleTitle.Text} successfully updated.", "Update Website", MessageBoxButton.OK);
                    NavigationService.GoBack();
                }
            }
            else
            {
                string task = "dbo.Update_Blog";
                DateTime accessedDate = (DateTime)accessDate.SelectedDateTime;
                DateTime postDate = (DateTime)postedDate.SelectedDateTime;

                TextBox[] requiredTextboxes = new TextBox[]
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
                    { "id", refToUpdate.Field<int>("Id").ToString() },
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
                var result = dataProcessor.PushBlogDetailsToDatabase(blogDetails, task);
                if (result == 1)
                {
                    MessageBox.Show($"{webArticleTitle.Text} successfully updated.", "Update Blog", MessageBoxButton.OK);
                    NavigationService.GoBack();
                }
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}