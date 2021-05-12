using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RefCatalogue.Controllers;

namespace RefCatalogue.Views.AddViews
{
    /// <summary>
    /// Interaction logic for NewJournal.xaml
    /// </summary>
    public partial class NewJournal : Page
    {
        private readonly DataProcessor dataProcessor = new();
        private const string Task = "dbo.Add_Journal";

        public NewJournal()
        {
            InitializeComponent();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors() == true)
            {
                return;
            }

            

            var journalDetails = new Dictionary<string, string>
            {
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

            var result = dataProcessor.PushJournalDetailsToDatabase(journalDetails, Task);
            if (result == 1)
            {
                MessageBox.Show($"{articleTitle.Text} successfully added.", "Add New Journal Article", MessageBoxButton.OK);
                FormHelper.ClearAllTextboxes(AddJournalPage);
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private bool CheckForErrors()
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
                return true;
            }

            return false;
        }
    }
}