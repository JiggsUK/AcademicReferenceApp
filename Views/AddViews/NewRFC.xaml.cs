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
    /// Interaction logic for NewRFC.xaml
    /// </summary>
    public partial class NewRFC : Page
    {
        private readonly DataProcessor dataProcessor = new DataProcessor();

        public NewRFC()
        {
            InitializeComponent();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors() == true)
            {
                return;
            }

            string task = "dbo.Add_RFC";

            var rfcDetails = new Dictionary<string, string>
            {
                { "RFCTitle", RFCArticleTitle.Text },
                { "rfc1first", RFC1first.Text },
                { "rfc1last", RFC1surname.Text },
                { "rfc2first", RFC2first.Text },
                { "rfc2last", RFC2surname.Text },
                { "rfc3first", RFC3first.Text },
                { "rfc3last", RFC3surname.Text },
                { "year", RFCYear.Text },
                { "RFCDocNumber", RFCDocNumber.Text },
                { "webURL", webURL.Text },
                { "accessDate", ((DateTime)accessDate.SelectedDateTime).ToString("dd MMMM yyyy")},
            };

            var result = dataProcessor.PushRFCDetailsToDatabase(rfcDetails, task);
            if (result == 1)
            {
                MessageBox.Show($"Successfully added {RFCArticleTitle.Text}.", "Add New RFC", MessageBoxButton.OK);
                FormHelper.ClearAllTextboxes(AddRFCPage);
            }
        }

        private bool CheckForErrors()
        {
            if (accessDate.SelectedDateTime == null)
            {
                MessageBox.Show("Accessed Date cannot be blank", "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }
            TextBox[] requiredTextboxes = new TextBox[]
            {
                RFCArticleTitle,
                RFCYear,
                webURL,
                RFCDocNumber
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