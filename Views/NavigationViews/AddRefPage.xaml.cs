using System.Windows;
using System.Windows.Controls;
using RefCatalogue.Controllers;

namespace RefCatalogue.Views.NavigationViews
{
    /// <summary>
    /// Interaction logic for AddRefPage.xaml
    /// </summary>
    public partial class AddRefPage : Page
    {
        public AddRefPage()
        {
            InitializeComponent();
        }

        private void NewBookClick(object sender, RoutedEventArgs e)
        {
            var newBookPage = new NewBook();
            NavigationService?.Navigate(newBookPage);
        }

        private void ViewRefsClick(object sender, RoutedEventArgs e)
        {
            var viewRefsPage = new ViewRef(new DataProcessor());
            NavigationService?.Navigate(viewRefsPage);
        }

        private void AddJournalClick(object sender, RoutedEventArgs e)
        {
            var newJournalPage = new NewJournal();
            NavigationService?.Navigate(newJournalPage);
        }

        private void AddConfPaperClick(object sender, RoutedEventArgs e)
        {
            var newConfPaperPage = new NewConferencePaper();
            NavigationService?.Navigate(newConfPaperPage);
        }

        private void AddWebsiteClick(object sender, RoutedEventArgs e)
        {
            var newWebsitePage = new NewWebArticle();
            NavigationService?.Navigate(newWebsitePage);
        }

        private void AddRfcClick(object sender, RoutedEventArgs e)
        {
            var newRfcPage = new NewRFC();
            NavigationService?.Navigate(newRfcPage);
        }

        private void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}