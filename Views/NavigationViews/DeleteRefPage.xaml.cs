using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using RefCatalogue.Controllers;

namespace RefCatalogue.Views.NavigationViews
{
    /// <summary>
    /// Interaction logic for DeleteRefPage.xaml
    /// </summary>
    public partial class DeleteRefPage : Page
    {
        private readonly IDataProcessor dataRetrieval;

        public DeleteRefPage(IDataProcessor dataRetrieval)
        {
            InitializeComponent();
            this.dataRetrieval = dataRetrieval ?? throw new ArgumentNullException(nameof(dataRetrieval));
            RefListData.ItemsSource = dataRetrieval.RetrieveAllReferences();
        }
        
        private void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (RefListData.SelectedItems.Count == 0)
            {
                MessageBox.Show($"Please select at least one reference to delete", "Delete References", MessageBoxButton.OK);
            }

            foreach (DataRowView reference in RefListData.SelectedItems)
            {
                var refToDelete = reference.Row;
                var tableName = refToDelete.Field<string>("RefType");

                if (refToDelete.Field<string>("RefType") == "Conf Paper")
                {
                    tableName = "ConfPaper";
                }

                dataRetrieval.DeleteReference(refToDelete.Field<int>("Id").ToString(), tableName);
                var deleteRef = new DeleteRefPage(new DataProcessor());
                NavigationService?.Navigate(deleteRef);
            }
        }
    }
}