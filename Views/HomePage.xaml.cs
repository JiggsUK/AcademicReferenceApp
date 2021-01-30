using RefCatalogue.Controllers;
using System.Windows;
using System.Windows.Controls;

namespace RefCatalogue.Views
{
    /// <summary>
    /// Interaction logic for RefCatHome.xaml
    /// </summary>
    public partial class RefCatHome : Page
    {
        public RefCatHome()
        {
            InitializeComponent();
        }

        private void ViewRefsClick(object sender, RoutedEventArgs e)
        {
            ViewRef viewRefsPage = new ViewRef(new DataProcessor());
            this.NavigationService.Navigate(viewRefsPage);
        }

        private void AddRefClick(object sender, RoutedEventArgs e)
        {
            AddRefPage addRef = new AddRefPage();
            this.NavigationService.Navigate(addRef);
        }

        private void UpdateRefClick(object sender, RoutedEventArgs e)
        {
            UpdateRefPage updateRef = new UpdateRefPage(new DataProcessor());
            this.NavigationService.Navigate(updateRef);
        }

        private void DeleteRefClick(object sender, RoutedEventArgs e)
        {
            DeleteRefPage deleteRef = new DeleteRefPage(new DataProcessor());
            this.NavigationService.Navigate(deleteRef);
        }
    }
}