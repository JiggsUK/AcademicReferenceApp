using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RefCatalogue.Controllers;

namespace RefCatalogue.Views.NavigationViews
{
    /// <summary>
    /// Interaction logic for ViewRef.xaml
    /// </summary>
    public partial class ViewRef : Page
    {
        private DataTable refsForExport;
        private DataView allRefs;

        public ViewRef(IDataProcessor dataRetrieval)
        {
            InitializeComponent();
            allRefs = dataRetrieval.RetrieveAllReferences();
            refListData.ItemsSource = allRefs;
        }
        
        private void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Button_Click_Export(object sender, RoutedEventArgs e)
        {
            refsForExport = allRefs.ToTable();

            if (refListData.SelectedItems.Count > 0)
            {
                var sortedReferences = refListData.Items.Cast<DataRowView>().Where(item => refListData.SelectedItems.Contains(item)).ToList();

                refsForExport.Clear();

                foreach (var row in sortedReferences.Select(rowView => rowView.Row))
                {
                    refsForExport.ImportRow(row);
                    refsForExport.AcceptChanges();
                }
            }

            var formattedrefs = Formatter.FormatEntries(refsForExport);
            Exporter.ExportToWord(formattedrefs);
        }
    }
}