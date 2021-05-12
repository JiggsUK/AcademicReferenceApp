using System.Collections.Generic;
using System.Data;

namespace RefCatalogue.Controllers
{
    public interface IDataProcessor
    {
        DataTable SelectAll(DataTable data);

        int PushBookDetailsToDatabase(Dictionary<string, string> bookDetails, string SqlCom);

        int PushConfPaperDetailsToDatabase(Dictionary<string, string> bookDetails, string SqlCom);

        int PushJournalDetailsToDatabase(Dictionary<string, string> bookDetails, string SqlCom);

        int PushRfcDetailsToDatabase(Dictionary<string, string> bookDetails, string SqlCom);

        int PushWebsiteDetailsToDatabase(Dictionary<string, string> bookDetails, string SqlCom);

        int PushBlogDetailsToDatabase(Dictionary<string, string> bookDetails, string SqlCom);

        void DeleteReference(string id, string tableName);
        DataView RetrieveAllReferences();
    }
}