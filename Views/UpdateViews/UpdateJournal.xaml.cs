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
    /// Interaction logic for UpdateJournal.xaml
    /// </summary>
    public partial class UpdateJournal : Page
    {
        private readonly DataProcessor _dataProcessor = new();
        private DataRow _refToUpdate;
        private const string Task = "dbo.Update_Journal";

        public UpdateJournal(DataRow selectedRow)
        {
            InitializeComponent();
            _refToUpdate = selectedRow;
            LoadTextboxData(_refToUpdate);
        }

        private void LoadTextboxData(DataRow data)
        {
            articleTitle.Text = data.Field<string>("ArticleTitle") ?? string.Empty;
            journalTitle.Text = data.Field<string>("JournalTitle") ?? string.Empty;
            JA1first.Text = data.Field<string>("Author1FN") ?? string.Empty;
            JA1surname.Text = data.Field<string>("Author1SN") ?? string.Empty;
            JA2first.Text = data.Field<string>("Author2FN") ?? string.Empty;
            JA2surname.Text = data.Field<string>("Author2SN") ?? string.Empty;
            JA3first.Text = data.Field<string>("Author3FN") ?? string.Empty;
            JA3surname.Text = data.Field<string>("Author3SN") ?? string.Empty;
            JA4first.Text = data.Field<string>("Author4FN") ?? string.Empty;
            JA4surname.Text = data.Field<string>("Author4SN") ?? string.Empty;
            journalYear.Text = data.Field<int>("Year").ToString();
            journalPartNo.Text = data.Field<int>("PartNo").ToString();
            journalVol.Text = data.Field<int>("Volume").ToString();
            pageFrom.Text = data.Field<int>("PageFrom").ToString();
            pageTo.Text = data.Field<int>("PageTo").ToString();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            var requiredTextboxes = new[]
            {
                articleTitle,
                journalTitle,
                JA1first,
                JA1surname,
                journalYear,
                pageFrom,
                pageTo,
                journalVol
            };

            var errors = FormHelper.ValidateTextboxes(requiredTextboxes);
            if (errors.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Missing Required Fields", MessageBoxButton.OK);
                return;
            }

            var journalDetails = new Dictionary<string, string>
            {
                { "id", _refToUpdate.Field<int>("Id").ToString() },
                { "articletitle", articleTitle.Text },
                { "journaltitle", journalTitle.Text },
                { "au1first", JA1first.Text },
                { "au1last", JA1surname.Text },
                { "au2first", JA2first.Text },
                { "au2last", JA2surname.Text },
                { "au3first", JA3first.Text },
                { "au3last", JA3surname.Text },
                { "au4first", JA4first.Text },
                { "au4last", JA4surname.Text },
                { "year", journalYear.Text },
                { "pageFrom", pageFrom.Text },
                { "pageTo", pageTo.Text },
                { "partNo", journalPartNo.Text },
                { "volume", journalVol.Text }
            };

            // Run update script
            var result = _dataProcessor.PushJournalDetailsToDatabase(journalDetails, Task);
            if (result == 1)
            {
                MessageBox.Show($"{articleTitle.Text} successfully updated.", "Update Journal", MessageBoxButton.OK);
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