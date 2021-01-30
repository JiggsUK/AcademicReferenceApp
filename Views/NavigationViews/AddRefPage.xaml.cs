using RefCatalogue.Controllers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RefCatalogue.Views
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
            NewBook newBookPage = new NewBook();
            this.NavigationService.Navigate(newBookPage);
        }

        private void ViewRefsClick(object sender, RoutedEventArgs e)
        {
            ViewRef viewRefsPage = new ViewRef(new DataProcessor());
            this.NavigationService.Navigate(viewRefsPage);
        }

        private void AddJournalClick(object sender, RoutedEventArgs e)
        {
            NewJournal newJournalPage = new NewJournal();
            this.NavigationService.Navigate(newJournalPage);
        }

        private void AddConfPaperClick(object sender, RoutedEventArgs e)
        {
            NewConferencePaper newConfPaperPage = new NewConferencePaper();
            this.NavigationService.Navigate(newConfPaperPage);
        }

        private void AddWebsiteClick(object sender, RoutedEventArgs e)
        {
            NewWebArticle newWebsitePage = new NewWebArticle();
            this.NavigationService.Navigate(newWebsitePage);
        }

        private void AddRFCClick(object sender, RoutedEventArgs e)
        {
            NewRFC newRFCPage = new NewRFC();
            this.NavigationService.Navigate(newRFCPage);
        }

        private void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}