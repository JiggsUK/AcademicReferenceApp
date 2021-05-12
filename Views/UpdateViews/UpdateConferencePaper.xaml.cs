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
    /// Interaction logic for UpdateConferencePaper.xaml
    /// </summary>
    public partial class UpdateConferencePaper : Page
    {
        private readonly DataProcessor _dataProcessor = new();
        private DataRow _refToUpdate;
        private const string Task = "dbo.Update_ConfPaper";

        public UpdateConferencePaper(DataRow selectedRow)
        {
            InitializeComponent();
            _refToUpdate = selectedRow;
            LoadTextboxData(selectedRow);
        }

        private void LoadTextboxData(DataRow data)
        {
            paperTitle.Text = data.Field<string>("PaperTitle") ?? string.Empty;
            CP1first.Text = data.Field<string>("Author1FN") ?? string.Empty;
            CP1surname.Text = data.Field<string>("Author1SN") ?? string.Empty;
            CP2first.Text = data.Field<string>("Author2FN") ?? string.Empty;
            CP2surname.Text = data.Field<string>("Author2SN") ?? string.Empty;
            CP3first.Text = data.Field<string>("Author3FN") ?? string.Empty;
            CP3surname.Text = data.Field<string>("Author3SN") ?? string.Empty;
            CP4first.Text = data.Field<string>("Author4FN") ?? string.Empty;
            CP4surname.Text = data.Field<string>("Author4SN") ?? string.Empty;
            confYear.Text = data.Field<int>("Year").ToString();
            confPub.Text = data.Field<string>("Publisher") ?? string.Empty;
            confPubLoc.Text = data.Field<string>("PubLocation") ?? string.Empty;
            confTitle.Text = data.Field<string>("ConfTitle")?? string.Empty;
            confSubTitle.Text = data.Field<string>("ConfSubTitle")?? string.Empty;
            confLoc.Text = data.Field<string>("ConfLocation")?? string.Empty;
            confDateFrom.Text = data.Field<string>("ConfDateFrom") ?? string.Empty;
            confDateTo.Text = data.Field<string>("ConfDateTo") ?? string.Empty;
            pageFrom.Text = data.Field<int>("PageFrom").ToString();
            pageTo.Text = data.Field<int>("PageTo").ToString();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            
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
                return;
            }
            
            var confPaperDetails = new Dictionary<string, string>
            {
                { "id", _refToUpdate.Field<int>("Id").ToString() },
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
                { "confDateFrom",confDateFrom.Text},
                { "confDateTo", confDateTo.Text }, 
                { "pageFrom", pageFrom.Text },
                { "pageTo", pageTo.Text },
                { "publisher", confPub.Text },
                { "publisherLoc", confPubLoc.Text }
            };

            // Run update script
            var result = _dataProcessor.PushConfPaperDetailsToDatabase(confPaperDetails, Task);
            if (result == 1)
            {
                MessageBox.Show($"{paperTitle.Text} successfully updated.", "Update Conference Paper", MessageBoxButton.OK);
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