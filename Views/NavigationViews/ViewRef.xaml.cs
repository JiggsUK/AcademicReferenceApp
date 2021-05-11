using RefCatalogue.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RefCatalogue.Views
{
    /// <summary>
    /// Interaction logic for ViewRef.xaml
    /// </summary>
    public partial class ViewRef : Page
    {
        private DataTable refsForExport;
        private DataView allRefs;
        private readonly IDataProcessor dataRetrieval;

        public ViewRef(IDataProcessor dataRetrieval)
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

        private void Button_Click_Export(object sender, RoutedEventArgs e)
        {
            refsForExport = allRefs.ToTable();

            if (refListData.SelectedItems.Count > 0)
            {
                List<DataRowView> sortedReferences = new List<DataRowView>();
                foreach (DataRowView item in refListData.Items)
                {
                    if (refListData.SelectedItems.Contains(item))
                    {
                        sortedReferences.Add(item);
                    }
                }

                refsForExport.Clear();

                foreach (DataRowView rowView in sortedReferences)
                {
                    DataRow row = rowView.Row;
                    refsForExport.ImportRow(row);
                    refsForExport.AcceptChanges();
                }
            }

            var formattedrefs = Formatter.FormatEntries(refsForExport);
            Exporter.ExportToWord(formattedrefs);
        }
    }
}