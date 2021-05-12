using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RefCatalogue.Controllers;
using RefCatalogue.Views.NavigationViews;

namespace RefCatalogue.Views.UpdateViews
{
    /// <summary>
    /// Interaction logic for UpdateRFC.xaml
    /// </summary>
    public partial class UpdateRfc : Page
    {
        private readonly DataProcessor _dataProcessor = new();
        private DataRow _refToUpdate;
        private const string Task = "dbo.Update_RFC";

        public UpdateRfc(DataRow selectedRow)
        {
            InitializeComponent();
            _refToUpdate = selectedRow;
            LoadTextboxData(selectedRow);
        }

        private void LoadTextboxData(DataRow data)
        {
            RFCArticleTitle.Text = data.Field<string>("RFCTitle") ?? string.Empty;
            RFC1first.Text = data.Field<string>("Author1FN") ?? string.Empty;
            RFC1surname.Text = data.Field<string>("Author1SN") ?? string.Empty;
            RFC2first.Text = data.Field<string>("Author2FN") ?? string.Empty;
            RFC2surname.Text = data.Field<string>("Author2SN") ?? string.Empty;
            RFC3first.Text = data.Field<string>("Author3FN") ?? string.Empty;
            RFC3surname.Text = data.Field<string>("Author3SN") ?? string.Empty;
            RFCYear.Text = data.Field<int>("Year").ToString();
            RFCDocNumber.Text = data.Field<int>("RFCDocNumber").ToString();
            webURL.Text = data.Field<string>("WebURL") ?? string.Empty;
            accessDate.SelectedDateTime = DateTime.Parse(data.Field<string>("accessDate") ?? string.Empty);
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            var accessedDate = (DateTime)accessDate.SelectedDateTime;

            var requiredTextboxes = new[]
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
                { "id", _refToUpdate.Field<int>("Id").ToString() },
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
            var result = _dataProcessor.PushRfcDetailsToDatabase(rfcDetails, Task);
            if (result == 1)
            {
                MessageBox.Show($"{RFCArticleTitle.Text} successfully updated.", "Update RFC", MessageBoxButton.OK);
                var updateRef = new UpdateRefPage(new DataProcessor());
                NavigationService?.Navigate(updateRef);
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}