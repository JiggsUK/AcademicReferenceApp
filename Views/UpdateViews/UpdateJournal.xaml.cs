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
    /// Interaction logic for UpdateJournal.xaml
    /// </summary>
    public partial class UpdateJournal : Page
    {
        private readonly DataProcessor dataProcessor = new DataProcessor();
        private DataRow refToUpdate;

        public UpdateJournal(DataRow refToUpdate)
        {
            InitializeComponent();
            this.refToUpdate = refToUpdate;
            LoadTextboxData(refToUpdate);
        }

        private void LoadTextboxData(DataRow refToUpdate)
        {
            articleTitle.Text = refToUpdate.Field<string>("PaperTitle");
            JA1first.Text = refToUpdate.Field<string>("Author1FN");
            JA1surname.Text = refToUpdate.Field<string>("Author1SN");
            JA2first.Text = refToUpdate.Field<string>("Author2FN");
            JA2surname.Text = refToUpdate.Field<string>("Author2SN");
            JA3first.Text = refToUpdate.Field<string>("Author3FN");
            JA3surname.Text = refToUpdate.Field<string>("Author3SN");
            JA4first.Text = refToUpdate.Field<string>("Author4FN");
            JA4surname.Text = refToUpdate.Field<string>("Author4SN");
            journalYear.Text = refToUpdate.Field<int>("Year").ToString();
            journalPartNo.Text = refToUpdate.Field<string>("PartNo");
            journalVol.Text = refToUpdate.Field<string>("Volume");
            pageFrom.Text = refToUpdate.Field<int>("PageFrom").ToString();
            pageTo.Text = refToUpdate.Field<int>("PageTo").ToString();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            string task = "dbo.Update_Journal";
            TextBox[] requiredTextboxes = new TextBox[]
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
                { "id", refToUpdate.Field<int>("Id").ToString() },
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
            var result = dataProcessor.PushJournalDetailsToDatabase(journalDetails, task);
            if (result == 1)
            {
                MessageBox.Show($"{articleTitle.Text} successfully updated.", "Update Journal", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}