using RefCatalogue.Controllers;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RefCatalogue.Views
{
    /// <summary>
    /// Interaction logic for DeleteRefPage.xaml
    /// </summary>
    public partial class DeleteRefPage : Page
    {
        private DataView allRefs;
        private readonly IDataProcessor dataRetrieval;

        public DeleteRefPage(IDataProcessor dataRetrieval)
        {
            InitializeComponent();
            this.dataRetrieval = dataRetrieval ?? throw new ArgumentNullException(nameof(dataRetrieval));
            refListData.ItemsSource = RetrieveAllReferences();
        }

        public DataView RetrieveAllReferences()
        {
            DataTable dt = new DataTable("allRefs");
            allRefs = dataRetrieval.SelectAll(dt).DefaultView;
            dt.DefaultView.Sort = "Author1SN asc";
            return allRefs;
        }

        private void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            // single select and button trigger to open dialog confirmation box
            // use yes confirmation click to trigger delete SP - use ID and reftype to get table and record

            if (refListData.Items == null)
            {
                MessageBox.Show($"Please select at least one reference to delete", "Delete References", MessageBoxButton.OK);
            }

            foreach (DataRowView reference in refListData.SelectedItems)
            {
                DataRow refToDelete = reference.Row;
                var tableName = refToDelete.Field<string>("RefType");

                if (refToDelete.Field<string>("RefType") == "Conf Paper")
                {
                    tableName = "ConfPaper";
                }

                dataRetrieval.DeleteReference(refToDelete.Field<int>("Id").ToString(), tableName);
            }
        }
    }
}