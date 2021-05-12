using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RefCatalogue.Controllers;

namespace RefCatalogue.Views.AddViews
{
    /// <summary>
    /// Interaction logic for NewWebsite.xaml
    /// </summary>
    public partial class NewWebArticle : Page
    {
        private readonly DataProcessor dataProcessor = new();
        private const string Task = "dbo.Add_Blog";

        public NewWebArticle()
        {
            InitializeComponent();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(blogSiteTitle.Text))
            {
                // Replace author1surname with organisation (if it is entered) to allow alphabetisation on View Refs
                var author1Surname = string.IsNullOrEmpty(organisation.Text) ? web1surname : organisation;

                if (CheckForErrorsWeb(author1Surname) == true)
                {
                    return;
                }

                var task = "dbo.Add_Website";
                var websiteDetails = new Dictionary<string, string>
                {
                    { "webpagetitle", webArticleTitle.Text },
                    { "web1first", web1first.Text },
                    { "web1last", author1Surname.Text },
                    { "web2first", web2first.Text },
                    { "web2last", web2surname.Text },
                    { "web3first", web3first.Text },
                    { "web3last", web3surname.Text },
                    { "year", webpageYear.Text },
                    { "webURL", webURL.Text },
                    { "accessDate", ((DateTime)accessDate.SelectedDateTime).ToString("dd MMMM yyyy")},
                };

                var result = dataProcessor.PushWebsiteDetailsToDatabase(websiteDetails, task);
                if (result == 1)
                {
                    MessageBox.Show($"Successfully added {webArticleTitle.Text}.", "Update Website", MessageBoxButton.OK);
                    FormHelper.ClearAllTextboxes(AddWebsitePage);
                }
            }
            else
            {
                if (CheckForErrorsBlog() == true)
                {
                    return;
                }

                

                var blogDetails = new Dictionary<string, string>
                {
                    { "articleTitle", webArticleTitle.Text },
                    { "web1first", web1first.Text },
                    { "web1last", web1surname.Text },
                    { "web2first", web2first.Text },
                    { "web2last", web2surname.Text },
                    { "web3first", web3first.Text },
                    { "web3last", web3surname.Text },
                    { "year", webpageYear.Text },
                    { "webURL", webURL.Text },
                    { "accessDate", ((DateTime)accessDate.SelectedDateTime).ToString("dd MMMM yyyy")},
                    { "websiteName", blogSiteTitle.Text},
                    { "postedDate", ((DateTime)postedDate.SelectedDateTime).ToString("dd MMMM")}
                };

                var result = dataProcessor.PushBlogDetailsToDatabase(blogDetails, Task);
                if (result == 1)
                {
                    MessageBox.Show($"Successfully added {webArticleTitle.Text}.", "Update Blog", MessageBoxButton.OK);
                    FormHelper.ClearAllTextboxes(AddWebsitePage);
                }
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private bool CheckForErrorsWeb(TextBox author1Surname)
        {
            if (accessDate.SelectedDateTime == null)
            {
                MessageBox.Show("Accessed Date cannot be blank", "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }

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
                return true;
            }

            return false;
        }

        private bool CheckForErrorsBlog()
        {
            if (accessDate.SelectedDateTime == null)
            {
                MessageBox.Show("Accessed Date cannot be blank", "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }

            if (postedDate.SelectedDateTime == null)
            {
                MessageBox.Show("Posted Date cannot be blank", "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }
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
                return true;
            }

            return false;
        }
    }
}