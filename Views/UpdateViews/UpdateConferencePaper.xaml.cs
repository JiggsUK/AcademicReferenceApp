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
    /// Interaction logic for UpdateConferencePaper.xaml
    /// </summary>
    public partial class UpdateConferencePaper : Page
    {
        private readonly DataProcessor dataProcessor = new DataProcessor();
        private DataRow refToUpdate;

        public UpdateConferencePaper(DataRow refToUpdate)
        {
            InitializeComponent();
            this.refToUpdate = refToUpdate;
            LoadTextboxData(refToUpdate);
        }

        private void LoadTextboxData(DataRow refToUpdate)
        {
            paperTitle.Text = refToUpdate.Field<string>("PaperTitle");
            CP1first.Text = refToUpdate.Field<string>("Author1FN");
            CP1surname.Text = refToUpdate.Field<string>("Author1SN");
            CP2first.Text = refToUpdate.Field<string>("Author2FN");
            CP2surname.Text = refToUpdate.Field<string>("Author2SN");
            CP3first.Text = refToUpdate.Field<string>("Author3FN");
            CP3surname.Text = refToUpdate.Field<string>("Author3SN");
            CP4first.Text = refToUpdate.Field<string>("Author4FN");
            CP4surname.Text = refToUpdate.Field<string>("Author4SN");
            confYear.Text = refToUpdate.Field<int>("Year").ToString();
            confPub.Text = refToUpdate.Field<string>("Publisher");
            confPubLoc.Text = refToUpdate.Field<string>("PubLocation");
            confTitle.Text = refToUpdate.Field<int>("ConfTitle").ToString();
            confSubTitle.Text = refToUpdate.Field<int>("ConfSubTitle").ToString();
            confLoc.Text = refToUpdate.Field<int>("ConfLocation").ToString();
            confDateFrom.SelectedDateTime = refToUpdate.Field<DateTime>("ConfDateFrom");
            confDateTo.SelectedDateTime = refToUpdate.Field<DateTime>("ConfDateTo");
            pageFrom.Text = refToUpdate.Field<int>("PageFrom").ToString();
            pageTo.Text = refToUpdate.Field<int>("PageTo").ToString();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            string task = "dbo.Update_ConfPaper";
            TextBox[] requiredTextboxes = new TextBox[]
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
                return;
            }

            DateTime dateFrom = (DateTime)confDateFrom.SelectedDateTime;
            DateTime dateTo = (DateTime)confDateTo.SelectedDateTime;

            var confPaperDetails = new Dictionary<string, string>
            {
                { "id", refToUpdate.Field<int>("Id").ToString() },
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
                { "confDateFrom", dateFrom.ToString("dd") }, // keep just the day
                { "confDateTo", dateTo.ToString("dd MMMM") }, // discard the year
                { "pageFrom", pageFrom.Text },
                { "pageTo", pageTo.Text },
                { "publisher", confPub.Text },
                { "publisherLoc", confPubLoc.Text }
            };

            // Run update script
            var result = dataProcessor.PushConfPaperDetailsToDatabase(confPaperDetails, task);
            if (result == 1)
            {
                MessageBox.Show($"{paperTitle.Text} successfully updated.", "Update Conference Paper", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}