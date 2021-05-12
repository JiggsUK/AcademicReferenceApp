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
    /// Interaction logic for UpdateBook.xaml
    /// </summary>
    public partial class UpdateBook : Page
    {
        private readonly DataProcessor _dataProcessor = new();
        private DataRow _refToUpdate;
        private const string Task = "dbo.Update_Book";

        public UpdateBook(DataRow selectedRow)
        {
            InitializeComponent();
            _refToUpdate = selectedRow;
            LoadTextboxData(selectedRow);
        }

        private void LoadTextboxData(DataRow data)
        {
            BookTitle.Text = data.Field<string>("BookTitle") ?? string.Empty;
            Author1FirstName.Text = data.Field<string>("Author1FN") ?? string.Empty;
            Author1Surname.Text = data.Field<string>("Author1SN") ?? string.Empty;
            Author2FirstName.Text = data.Field<string>("Author2FN") ?? string.Empty;
            Author2Surname.Text = data.Field<string>("Author2SN") ?? string.Empty;
            Author3FirstName.Text = data.Field<string>("Author3FN") ?? string.Empty;
            Author3Surname.Text = data.Field<string>("Author3SN") ?? string.Empty;
            Author4FirstName.Text = data.Field<string>("Author4FN") ?? string.Empty;
            Author4Surname.Text = data.Field<string>("Author4SN") ?? string.Empty;
            Year.Text = data.Field<int>("Year").ToString();
            Publisher.Text = data.Field<string>("Publisher") ?? string.Empty;
            PublishingLocation.Text = data.Field<string>("PublisherLoc") ?? string.Empty;
            Edition.Text = data.Field<int>("Edition").ToString();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            
            var requiredTextboxes = new[]
            {
                BookTitle,
                Author1FirstName,
                Author1Surname,
                Year
            };

            var errors = FormHelper.ValidateTextboxes(requiredTextboxes);
            if (errors.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Missing Required Fields", MessageBoxButton.OK);
                return;
            }

            var bookDetails = new Dictionary<string, string>
            {
                { "id", _refToUpdate.Field<int>("Id").ToString() },
                { "title", BookTitle.Text },
                { "au1first", Author1FirstName.Text },
                { "au1last", Author1Surname.Text },
                { "au2first", Author2FirstName.Text },
                { "au2last", Author2Surname.Text },
                { "au3first", Author3FirstName.Text },
                { "au3last", Author3Surname.Text },
                { "au4first", Author4FirstName.Text },
                { "au4last", Author4Surname.Text },
                { "year", Year.Text },
                { "publisher", Publisher.Text },
                { "publisherLoc", PublishingLocation.Text },
                { "edition", Edition.Text }
            };

            // Run update script
            var result = _dataProcessor.PushBookDetailsToDatabase(bookDetails, Task);
            if (result == 1)
            {
                MessageBox.Show($"{BookTitle.Text} successfully updated.", "Update Book", MessageBoxButton.OK);
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