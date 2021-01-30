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
    /// Interaction logic for UpdateRFC.xaml
    /// </summary>
    public partial class UpdateRFC : Page
    {
        private readonly DataProcessor dataProcessor = new DataProcessor();
        private DataRow refToUpdate;

        public UpdateRFC(DataRow refToUpdate)
        {
            InitializeComponent();
            this.refToUpdate = refToUpdate;
            LoadTextboxData(refToUpdate);
        }

        private void LoadTextboxData(DataRow refToUpdate)
        {
            RFCArticleTitle.Text = refToUpdate.Field<string>("PaperTitle");
            RFC1first.Text = refToUpdate.Field<string>("Author1FN");
            RFC1surname.Text = refToUpdate.Field<string>("Author1SN");
            RFC2first.Text = refToUpdate.Field<string>("Author2FN");
            RFC2surname.Text = refToUpdate.Field<string>("Author2SN");
            RFC3first.Text = refToUpdate.Field<string>("Author3FN");
            RFC3surname.Text = refToUpdate.Field<string>("Author3SN");
            RFCYear.Text = refToUpdate.Field<int>("Year").ToString();
            RFCDocNumber.Text = refToUpdate.Field<string>("RFCDocNumber");
            webURL.Text = refToUpdate.Field<string>("WebURL");
            accessDate.SelectedDateTime = refToUpdate.Field<DateTime>("accessDate");
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            string task = "dbo.Update_RFC";
            DateTime accessedDate = (DateTime)accessDate.SelectedDateTime;

            TextBox[] requiredTextboxes = new TextBox[]
            {
                RFCArticleTitle,
                RFCYear,
                webURL,
                RFCDocNumber
            };

            var errors = FormHelper.ValidateTextboxes(requiredTextboxes);
            if (errors.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Missing Required Fields", MessageBoxButton.OK);
                return;
            }

            var rfcDetails = new Dictionary<string, string>
            {
                { "id", refToUpdate.Field<int>("Id").ToString() },
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
                { "accessDate", accessedDate.ToString("dd MMMM yyyy")},
            };

            // Run update script
            var result = dataProcessor.PushRFCDetailsToDatabase(rfcDetails, task);
            if (result == 1)
            {
                MessageBox.Show($"{RFCArticleTitle.Text} successfully updated.", "Update RFC", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}