using RefCatalogue.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RefCatalogue.Views
{
    /// <summary>
    /// Interaction logic for RefReport.xaml
    /// </summary>
    public partial class NewBook : Page
    {
        private readonly DataProcessor dataProcessor = new DataProcessor();

        public NewBook()
        {
            InitializeComponent();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors() == true)
            {
                return;
            }

            string task = "dbo.Add_Book";

            var bookDetails = new Dictionary<string, string>
            {
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

            var result = dataProcessor.PushBookDetailsToDatabase(bookDetails, task);
            if (result == 1)
            {
                MessageBox.Show($"{bookTitle.Text} successfully added.", "Add New Book", MessageBoxButton.OK);
                FormHelper.ClearAllTextboxes(AddBookPage);
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private bool CheckForErrors()
        {
            TextBox[] requiredTextboxes = new TextBox[]
            {
                bookTitle,
                Author1FirstName,
                Author1Surname,
                Year
            };

            List<string> errors = FormHelper.ValidateTextboxes(requiredTextboxes);
            if (errors.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Missing Required Fields", MessageBoxButton.OK);
                return true;
            }

            return false;
        }
    }
}