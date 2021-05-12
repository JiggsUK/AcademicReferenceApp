using RefCatalogue.Controllers;
using System.Windows;
using RefCatalogue.Views.NavigationViews;

namespace RefCatalogue.Views
{
    /// <summary>
    /// Interaction logic for RefCatHome.xaml
    /// </summary>
    public partial class RefCatHome
    {
        public RefCatHome()
        {
            InitializeComponent();
        }

        private void ViewRefsClick(object sender, RoutedEventArgs e)
        {
            var viewRefsPage = new ViewRef(new DataProcessor());
            NavigationService?.Navigate(viewRefsPage);
        }

        private void AddRefClick(object sender, RoutedEventArgs e)
        {
            var addRef = new AddRefPage();
            NavigationService?.Navigate(addRef);
        }

        private void UpdateRefClick(object sender, RoutedEventArgs e)
        {
            var updateRef = new UpdateRefPage(new DataProcessor());
            NavigationService?.Navigate(updateRef);
        }

        private void DeleteRefClick(object sender, RoutedEventArgs e)
        {
            var deleteRef = new DeleteRefPage(new DataProcessor());
            NavigationService?.Navigate(deleteRef);
        }
    }
}