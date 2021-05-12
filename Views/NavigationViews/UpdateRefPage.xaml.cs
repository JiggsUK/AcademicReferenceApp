using System.Data;
using System.Windows;
using System.Windows.Controls;
using RefCatalogue.Controllers;
using RefCatalogue.Views.UpdateViews;

namespace RefCatalogue.Views.NavigationViews
{
    /// <summary>
    /// Interaction logic for UpdateRefPage.xaml
    /// </summary>
    public partial class UpdateRefPage : Page
    {

        public UpdateRefPage(IDataProcessor dataRetrieval)
        {
            InitializeComponent();
            refListData.ItemsSource = dataRetrieval.RetrieveAllReferences();
        }

        private void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (refListData.SelectedItem != null)
            {
                var selectedRef = (DataRowView)refListData.SelectedItem;
                var refUpdate = selectedRef.Row;

                if (refUpdate.Field<string>("RefType") == "Book")
                {
                    var updateBookView = new UpdateBook(refUpdate);
                    NavigationService?.Navigate(updateBookView);
                }
                else if (refUpdate.Field<string>("RefType") == "Journal")
                {
                    var updateJournalView = new UpdateJournal(refUpdate);
                    NavigationService?.Navigate(updateJournalView);
                }
                else if (refUpdate.Field<string>("RefType") == "Conf Paper")
                {
                    var updateConferencePaperView = new UpdateConferencePaper(refUpdate);
                    NavigationService?.Navigate(updateConferencePaperView);
                }
                else if (refUpdate.Field<string>("RefType") == "Website")
                {
                    var updateWebsiteView = new UpdateWebArticle(refUpdate);
                    NavigationService?.Navigate(updateWebsiteView);
                }
                else if (refUpdate.Field<string>("RefType") == "Blog")
                {
                    var updateBlogView = new UpdateWebArticle(refUpdate);
                    NavigationService?.Navigate(updateBlogView);
                }
                else if (refUpdate.Field<string>("RefType") == "RFC")
                {
                    var updateRfcView = new UpdateRfc(refUpdate);
                    NavigationService?.Navigate(updateRfcView);
                }
            }
            else
            {
                MessageBox.Show($"Please select at least one reference to update", "Missing Required Fields", MessageBoxButton.OK);
            }
        }
    }
}