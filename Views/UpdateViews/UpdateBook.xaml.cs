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
    /// Interaction logic for UpdateBook.xaml
    /// </summary>
    public partial class UpdateBook : Page
    {
        private readonly DataProcessor dataProcessor = new DataProcessor();
        private DataRow refToUpdate;

        public UpdateBook(DataRow refToUpdate)
        {
            InitializeComponent();
            this.refToUpdate = refToUpdate;
            LoadTextboxData(refToUpdate);
        }

        private void LoadTextboxData(DataRow refToUpdate)
        {
            bookTitle.Text = refToUpdate.Field<string>("BookTitle");
            Author1FirstName.Text = refToUpdate.Field<string>("Author1FN");
            Author1Surname.Text = refToUpdate.Field<string>("Author1SN");
            Author2FirstName.Text = refToUpdate.Field<string>("Author2FN");
            Author2Surname.Text = refToUpdate.Field<string>("Author2SN");
            Author3FirstName.Text = refToUpdate.Field<string>("Author3FN");
            Author3Surname.Text = refToUpdate.Field<string>("Author3SN");
            Author4FirstName.Text = refToUpdate.Field<string>("Author4FN");
            Author4Surname.Text = refToUpdate.Field<string>("Author4SN");
            Year.Text = refToUpdate.Field<int>("Year").ToString();
            Publisher.Text = refToUpdate.Field<string>("Publisher");
            PublishingLocation.Text = refToUpdate.Field<string>("PublisherLoc");
            Edition.Text = refToUpdate.Field<int>("Edition").ToString();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            string task = "dbo.Update_Book";
            TextBox[] requiredTextboxes = new TextBox[]
            {
                bookTitle,
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
                { "id", refToUpdate.Field<int>("Id").ToString() },
                { "title", bookTitle.Text },
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
            var result = dataProcessor.PushBookDetailsToDatabase(bookDetails, task);
            if (result == 1)
            {
                MessageBox.Show($"{bookTitle.Text} successfully updated.", "Update Book", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}