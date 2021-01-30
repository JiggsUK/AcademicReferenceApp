using RefCatalogue.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RefCatalogue.Views
{
    /// <summary>
    /// Interaction logic for NewWebsite.xaml
    /// </summary>
    public partial class NewWebArticle : Page
    {
        private readonly DataProcessor dataProcessor = new DataProcessor();

        public NewWebArticle()
        {
            InitializeComponent();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(blogSiteTitle.Text))
            {
                // Replace author1surname with organisation (if it is entered) to allow alphabetisation on View Refs
                TextBox Author1Surname = string.IsNullOrEmpty(organisation.Text) ? web1surname : organisation;

                if (CheckForErrorsWeb(Author1Surname) == true)
                {
                    return;
                }

                string task = "dbo.Add_Website";
                var websiteDetails = new Dictionary<string, string>
                {
                    { "webpagetitle", webArticleTitle.Text },
                    { "web1first", web1first.Text },
                    { "web1last", Author1Surname.Text },
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

                string task = "dbo.Add_Blog";

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

                var result = dataProcessor.PushBlogDetailsToDatabase(blogDetails, task);
                if (result == 1)
                {
                    MessageBox.Show($"Successfully added {webArticleTitle.Text}.", "Update Blog", MessageBoxButton.OK);
                    FormHelper.ClearAllTextboxes(AddWebsitePage);
                }
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private bool CheckForErrorsWeb(TextBox Author1Surname)
        {
            if (accessDate.SelectedDateTime == null)
            {
                MessageBox.Show("Accessed Date cannot be blank", "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }

            TextBox[] requiredTextboxes = new TextBox[]
                {
                webArticleTitle,
                web1first,
                Author1Surname,
                webpageYear,
                webURL
                };

            List<string> errors = FormHelper.ValidateTextboxes(requiredTextboxes);
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
            TextBox[] requiredTextboxes = new TextBox[]
            {
                webArticleTitle,
                web1first,
                web1surname,
                webpageYear,
                webURL,
                blogSiteTitle
            };

            List<string> errors = FormHelper.ValidateTextboxes(requiredTextboxes);
            if (errors.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }

            return false;
        }
    }
}