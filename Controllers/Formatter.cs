using System;
using System.Data;

namespace RefCatalogue.Controllers
{
    internal static class Formatter
    {
        /// <summary>
        /// Create a list of reference entries, formatted to the correct styling (Uni of Suffolk Harvard style)
        /// </summary>
        /// <param name="referenceDetails"></param>
        public static string[,] FormatEntries(DataTable referenceDetails)
        {
            var references = new string[referenceDetails.Rows.Count, 2];
            var rows = referenceDetails.Select();
            for (var i = 0; i <= referenceDetails.Rows.Count - 1; i++)
            {
                if (rows[i].Field<string>("RefType") == "Book")
                {
                    FormatBookEntry(references, rows, i);
                }
                else if (rows[i].Field<string>("RefType") == "Journal")
                {
                    FormatJournalEntry(references, rows, i);
                }
                else if (rows[i].Field<string>("RefType") == "Conf Paper")
                {
                    FormatConfPaperEntry(references, rows, i);
                }
                else if (rows[i].Field<string>("RefType") == "Website")
                {
                    FormatWebsiteEntry(references, rows, i);
                }
                else if (rows[i].Field<string>("RefType") == "Blog")
                {
                    FormatBlogEntry(references, rows, i);
                }
                else if (rows[i].Field<string>("RefType") == "RFC")
                {
                    FormatRfcEntry(references, rows, i);
                }
                else
                {
                    references[i, 0] = rows[i].Field<string>("RefType");
                    references[i, 1] = $"No format set for reference type: {rows[i].Field<string>("RefType")}.";
                }
            }

            return references;
        }

        private static void FormatBookEntry(string[,] references, DataRow[] rows, int i)
        {
            references[i, 0] = rows[i].Field<string>("RefType");

            var hasEdition = rows[i].Field<int>("Edition") > 0;
            string ednSuffix;

            if (rows[i].Field<int>("Edition") == 1)
            {
                ednSuffix = "st";
            }
            else if (rows[i].Field<int>("Edition") == 2)
            {
                ednSuffix = "nd";
            }
            else if (rows[i].Field<int>("Edition") == 3)
            {
                ednSuffix = "rd";
            }
            else
            {
                ednSuffix = "th";
            }

            string entry;
            if (rows[i].Field<string>("Author2SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("BookTitle")}.{(hasEdition ? Convert.ToString($"{rows[i].Field<int>("Edition")}{ednSuffix} edn. ") : " ")}{rows[i].Field<string>("PublisherLoc")}: {rows[i].Field<string>("Publisher")}.";
            }
            else if (rows[i].Field<string>("Author3SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. and {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("BookTitle")}.{(hasEdition ? Convert.ToString($"{rows[i].Field<int>("Edition")}{ednSuffix} edn. ") : " ")}{rows[i].Field<string>("PublisherLoc")}: {rows[i].Field<string>("Publisher")}.";
            }
            else if (rows[i].Field<string>("Author4SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}., {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. and {rows[i].Field<string>("Author3SN")}, {rows[i].Field<string>("Author3FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("BookTitle")}.{(hasEdition ? Convert.ToString($"{rows[i].Field<int>("Edition")}{ednSuffix} edn. ") : " ")}{rows[i].Field<string>("PublisherLoc")}: {rows[i].Field<string>("Publisher")}.";
            }
            else
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}., {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}., {rows[i].Field<string>("Author3SN")}, {rows[i].Field<string>("Author3FN")[..1]}. and {rows[i].Field<string>("Author4SN")}, {rows[i].Field<string>("Author4FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("BookTitle")}. {(hasEdition ? Convert.ToString($"{rows[i].Field<int>("Edition")}{ednSuffix} edn. ") : "")}{rows[i].Field<string>("PublisherLoc")}: {rows[i].Field<string>("Publisher")}.";
            }

            references[i, 1] = entry;
        }

        private static void FormatJournalEntry(string[,] references, DataRow[] rows, int i)
        {
            references[i, 0] = rows[i].Field<string>("RefType");

            string entry;
            if (rows[i].Field<string>("Author2SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("ArticleTitle")}', {rows[i].Field<string>("JournalTitle")}, {rows[i].Field<int>("Volume")}({rows[i].Field<int>("PartNo")}), pp. {rows[i].Field<int>("PageFrom")}-{rows[i].Field<int>("PageTo")}.";
            }
            else if (rows[i].Field<string>("Author3SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. and {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("ArticleTitle")}', {rows[i].Field<string>("JournalTitle")}, {rows[i].Field<int>("Volume")}({rows[i].Field<int>("PartNo")}), pp. {rows[i].Field<int>("PageFrom")}-{rows[i].Field<int>("PageTo")}.";
            }
            else if (rows[i].Field<string>("Author4SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}., {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. and {rows[i].Field<string>("Author3SN")}, {rows[i].Field<string>("Author3FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("ArticleTitle")}', {rows[i].Field<string>("JournalTitle")}, {rows[i].Field<int>("Volume")}({rows[i].Field<int>("PartNo")}), pp. {rows[i].Field<int>("PageFrom")}-{rows[i].Field<int>("PageTo")}.";
            }
            else
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. et al. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("ArticleTitle")}', {rows[i].Field<string>("JournalTitle")}, {rows[i].Field<int>("Volume")}({rows[i].Field<int>("PartNo")}), pp. {rows[i].Field<int>("PageFrom")}-{rows[i].Field<int>("PageTo")}.";
            }

            references[i, 1] = entry;
        }

        private static void FormatConfPaperEntry(string[,] references, DataRow[] rows, int i)
        {
            references[i, 0] = rows[i].Field<string>("RefType");

            string entry;
            if (rows[i].Field<string>("Author2SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("PaperTitle")}', {rows[i].Field<string>("ConfTitle")}{(string.IsNullOrEmpty(rows[i].Field<string>("ConfSubTitle")) ? "" : $": {rows[i].Field<string>("ConfSubTitle")}")}. {rows[i].Field<string>("ConfLocation")}, {rows[i].Field<string>("ConfDateFrom")}-{rows[i].Field<string>("ConfDateTo")}. {rows[i].Field<string>("PubLocation")}: {rows[i].Field<string>("Publisher")}, pp. {rows[i].Field<int>("PageFrom")}-{rows[i].Field<int>("PageTo")}.";
            }
            else if (rows[i].Field<string>("Author3SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. and {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("PaperTitle")}', {rows[i].Field<string>("ConfTitle")}{(string.IsNullOrEmpty(rows[i].Field<string>("ConfSubTitle")) ? "" : $": {rows[i].Field<string>("ConfSubTitle")}")}. {rows[i].Field<string>("ConfLocation")}, {rows[i].Field<string>("ConfDateFrom")}-{rows[i].Field<string>("ConfDateTo")}. {rows[i].Field<string>("PubLocation")}: {rows[i].Field<string>("Publisher")}, pp. {rows[i].Field<int>("PageFrom")}-{rows[i].Field<int>("PageTo")}.";
            }
            else if (rows[i].Field<string>("Author4SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}., {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. and {rows[i].Field<string>("Author3SN")}, {rows[i].Field<string>("Author3FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("PaperTitle")}', {rows[i].Field<string>("ConfTitle")}{(string.IsNullOrEmpty(rows[i].Field<string>("ConfSubTitle")) ? "" : $": {rows[i].Field<string>("ConfSubTitle")}")}. {rows[i].Field<string>("ConfLocation")}, {rows[i].Field<string>("ConfDateFrom")}-{rows[i].Field<string>("ConfDateTo")}. {rows[i].Field<string>("PubLocation")}: {rows[i].Field<string>("Publisher")}, pp. {rows[i].Field<int>("PageFrom")}-{rows[i].Field<int>("PageTo")}.";
            }
            else
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. et al. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("PaperTitle")}', {rows[i].Field<string>("ConfTitle")}{(string.IsNullOrEmpty(rows[i].Field<string>("ConfSubTitle")) ? "" : $": {rows[i].Field<string>("ConfSubTitle")}")}. {rows[i].Field<string>("ConfLocation")}, {rows[i].Field<string>("ConfDateFrom")}-{rows[i].Field<string>("ConfDateTo")}. {rows[i].Field<string>("PubLocation")}: {rows[i].Field<string>("Publisher")}, pp. {rows[i].Field<int>("PageFrom")}-{rows[i].Field<int>("PageTo")}.";
            }

            references[i, 1] = entry;
        }

        private static void FormatWebsiteEntry(string[,] references, DataRow[] rows, int i)
        {
            references[i, 0] = rows[i].Field<string>("RefType");

            string entry;
            if (rows[i].Field<string>("Author1FN") == null) // a.k.a an organisation as author
            {
                entry = $"{rows[i].Field<string>("Author1SN")} ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("WebpageTitle")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else if (rows[i].Field<string>("Author2SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("WebpageTitle")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else if (rows[i].Field<string>("Author3SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. and {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("WebpageTitle")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else if (rows[i].Field<string>("Author3SN") != null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}., {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. and {rows[i].Field<string>("Author3SN")}, {rows[i].Field<string>("Author3FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("WebpageTitle")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else
            {
                entry = $"{rows[i].Field<string>("WebpageTitle")} ({rows[i].Field<int>("Year")}) Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }

            references[i, 1] = entry;
        }

        private static void FormatBlogEntry(string[,] references, DataRow[] rows, int i)
        {
            references[i, 0] = rows[i].Field<string>("RefType");

            string entry;
            if (rows[i].Field<string>("Author2SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("WebpageTitle")}', {rows[i].Field<string>("blogSiteTitle")}, {rows[i].Field<string>("postedDate")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else if (rows[i].Field<string>("Author3SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. and {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("WebpageTitle")}', {rows[i].Field<string>("blogSiteTitle")}, {rows[i].Field<string>("postedDate")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}., {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. and {rows[i].Field<string>("Author3SN")}, {rows[i].Field<string>("Author3FN")[..1]}. ({rows[i].Field<int>("Year")}) '{rows[i].Field<string>("WebpageTitle")}', {rows[i].Field<string>("blogSiteTitle")}, {rows[i].Field<string>("postedDate")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }

            references[i, 1] = entry;
        }

        private static void FormatRfcEntry(string[,] references, DataRow[] rows, int i)
        {
            references[i, 0] = rows[i].Field<string>("RefType");

            string entry;
            if (rows[i].Field<string>("Author2SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("RFCTitle")}. RFC {rows[i].Field<int>("RFCDocNumber")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else if (rows[i].Field<string>("Author3SN") == null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}. and {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("RFCTitle")}. RFC {rows[i].Field<int>("RFCDocNumber")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else if (rows[i].Field<string>("Author3SN") != null)
            {
                entry = $"{rows[i].Field<string>("Author1SN")}, {rows[i].Field<string>("Author1FN")[..1]}., {rows[i].Field<string>("Author2SN")}, {rows[i].Field<string>("Author2FN")[..1]}. and {rows[i].Field<string>("Author3SN")}, {rows[i].Field<string>("Author3FN")[..1]}. ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("RFCTitle")}. RFC {rows[i].Field<int>("RFCDocNumber")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }
            else
            {
                entry = $"{rows[i].Field<string>("WebpageTitle")} ({rows[i].Field<int>("Year")}) {rows[i].Field<string>("RFCTitle")}. RFC {rows[i].Field<string>("RFCDocNumber")}. Available at: {rows[i].Field<string>("WebURL")} (Accessed: {rows[i].Field<string>("accessDate")}).";
            }

            references[i, 1] = entry;
        }
    }
}