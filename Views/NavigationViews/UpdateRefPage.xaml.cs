using RefCatalogue.Controllers;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RefCatalogue.Views
{
    /// <summary>
    /// Interaction logic for UpdateRefPage.xaml
    /// </summary>
    public partial class UpdateRefPage : Page
    {
        private DataView allRefs;
        private readonly IDataProcessor dataRetrieval;

        public UpdateRefPage(IDataProcessor dataRetrieval)
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

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            // single select and button trigger to open form
            // use RefType to open correct form -- need a longish if/ else if sequence
            // set textbox fields text == to the db values --- use dataIndex.Field<string>("BookTitle") to assign text to textboxes
            // save = record update
            // problem is how to display correct form - go off ref type??

            //refsToUpdate = allRefs.ToTable();
            //refsToUpdate.PrimaryKey = new DataColumn[] { refsToUpdate.Columns["ID"], refsToUpdate.Columns["RefType"] };

            if (refListData.SelectedItem != null)
            {
                DataRowView selectedRef = (DataRowView)refListData.SelectedItem;
                DataRow refUpdate = selectedRef.Row;

                if (refUpdate.Field<string>("RefType") == "Book")
                {
                    UpdateBook updateBookView = new UpdateBook(refUpdate);
                    this.NavigationService.Navigate(updateBookView);
                }
                else if (refUpdate.Field<string>("RefType") == "Journal")
                {
                    UpdateJournal updateJournalView = new UpdateJournal(refUpdate);
                    this.NavigationService.Navigate(updateJournalView);
                }
                else if (refUpdate.Field<string>("RefType") == "Conf Paper")
                {
                    UpdateConferencePaper updateConferencePaperView = new UpdateConferencePaper(refUpdate);
                    this.NavigationService.Navigate(updateConferencePaperView);
                }
                else if (refUpdate.Field<string>("RefType") == "Website")
                {
                    UpdateWebArticle updateWebsiteView = new UpdateWebArticle(refUpdate);
                    this.NavigationService.Navigate(updateWebsiteView);
                }
                else if (refUpdate.Field<string>("RefType") == "Blog")
                {
                    UpdateWebArticle updateBlogView = new UpdateWebArticle(refUpdate);
                    this.NavigationService.Navigate(updateBlogView);
                }
                else if (refUpdate.Field<string>("RefType") == "RFC")
                {
                    UpdateRFC updateRFCView = new UpdateRFC(refUpdate);
                    this.NavigationService.Navigate(updateRFCView);
                }
                else
                {
                    // nothing to do
                }
            }
            else
            {
                MessageBox.Show($"Please select at least one reference to update", "Missing Required Fields", MessageBoxButton.OK);
            }
        }
    }
}