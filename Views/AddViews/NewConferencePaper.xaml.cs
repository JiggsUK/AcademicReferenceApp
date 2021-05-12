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
    ///
    public partial class NewConferencePaper : Page
    {
        private readonly DataProcessor dataProcessor = new();
        private const string Task = "dbo.Add_ConfPaper";

        public NewConferencePaper()
        {
            InitializeComponent();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                return;
            }

            
            var confPaperDetails = new Dictionary<string, string>
            {
                { "paperTitle", paperTitle.Text },
                { "au1first", CP1first.Text },
                { "au1last", CP1surname.Text },
                { "au2first", CP2first.Text },
                { "au2last", CP2surname.Text },
                { "au3first", CP3first.Text },
                { "au3last", CP3surname.Text },
                { "au4first", CP4first.Text },
                { "au4last", CP4surname.Text },
                { "year", confYear.Text },
                { "confTitle", confTitle.Text },
                { "confSubTitle", confSubTitle.Text },
                { "confLoc", confLoc.Text },
                { "confDateFrom", ((DateTime)confDateFrom.SelectedDateTime).ToString("dd") }, // keep just the day
                { "confDateTo", ((DateTime)confDateTo.SelectedDateTime).ToString("dd MMMM") }, // discard the year
                { "pageFrom", pageFrom.Text },
                { "pageTo", pageTo.Text },
                { "publisher", confPub.Text },
                { "publisherLoc", confPubLoc.Text }
            };

            var result = dataProcessor.PushConfPaperDetailsToDatabase(confPaperDetails, Task);
            if (result == 1)
            {
                MessageBox.Show($"Successfully added: {paperTitle.Text}.", "Add New Conference Paper", MessageBoxButton.OK);
                FormHelper.ClearAllTextboxes(AddConferencePaperPage);
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private bool CheckForErrors()
        {
            if (confDateFrom.SelectedDateTime == null)
            {
                MessageBox.Show("Conference Start Date cannot be blank", "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }

            if (confDateTo.SelectedDateTime == null)
            {
                MessageBox.Show("Conference End Date cannot be blank", "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }
            var requiredTextboxes = new[]
            {
                paperTitle,
                confTitle,
                CP1first,
                CP1surname,
                confYear,
                pageFrom,
                pageTo,
                confLoc,
                confPub,
                confPubLoc
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