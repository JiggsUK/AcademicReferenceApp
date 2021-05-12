using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace RefCatalogue.Controllers
{
    internal static class Exporter
    {
        public static void ExportToWord(string[,] referenceList, string filename)
        {
            // Open a WordprocessingDocument for editing using the file path.
            var wordDoc =
                WordprocessingDocument.Create(filename, WordprocessingDocumentType.Document, true);
            // Assign a reference to the existing document body.
            MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();

            // Create the document structure and add some text.
            mainPart.Document = new Document();
            Body body = mainPart.Document.AppendChild(new Body());
            //var body = wordprocessingDocument.MainDocumentPart.Document.Body;

            for (var i = 0; i <= referenceList.GetLength(0) - 1; i++)
            {
                var para = body.AppendChild(new Paragraph());
                var reference = referenceList[i, 1];
                if (referenceList[i, 0] == "Book")
                {
                    // find the index to start italics - the closing bracket on the year attribute
                    var italicStart = reference.IndexOf(')') + 2;

                    // find the index to end the italics - 1st fullstop after the start index
                    var italicEnd = reference.IndexOf('.', italicStart) + 1;
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "Journal")
                {
                    // find the index to start italics - the 1st apostrophe/comma combo (',)
                    var italicStart = referenceList[i, 1].IndexOf("',", StringComparison.Ordinal) + 2;

                    // find the index to end the italics - 1st comma after the start index
                    var italicEnd = referenceList[i, 1].IndexOf(',', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "Conf Paper")
                {
                    // find the index to start italics - the 1st apostrophe/comma combo (',)
                    var italicStart = referenceList[i, 1].IndexOf("',") + 2;

                    // find the index to end the italics - 1st fullstop after the start index
                    var italicEnd = referenceList[i, 1].IndexOf('.', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "Website")
                {
                    // find the index to start italics - the closing bracket on the year attribute
                    var italicStart = referenceList[i, 1].IndexOf(')') + 1;

                    // find the index to end the italics - 1st fullstop after the start index
                    var italicEnd = referenceList[i, 1].IndexOf('.', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "Blog")
                {
                    // find the index to start italics - the 1st apostrophe/comma combo (',)
                    var italicStart = referenceList[i, 1].IndexOf("',") + 2;

                    // find the index to end the italics - 1st fullstop after the start index
                    var italicEnd = referenceList[i, 1].IndexOf(',', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "RFC")
                {
                    // find the index to start italics - the closing bracket on the year attribute
                    var italicStart = referenceList[i, 1].IndexOf(')') + 1;

                    // find the index to end the italics - 1st fullstop after the start index
                    var italicEnd = referenceList[i, 1].IndexOf('.', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else
                {
                    var run = para.AppendChild(new Run());
                    var txt = new Text
                    {
                        Text = referenceList[i, 1],
                        Space = SpaceProcessingModeValues.Preserve
                    };
                    run.AppendChild(txt);
                }
            }
            wordDoc.Save();
            wordDoc.Close();
        }

        private static void AddRuns(int italicStart, int italicEnd, string txt, int i, Paragraph para)
        {
            var preItalics = para.AppendChild(new Run());
            var italics = para.AppendChild(new Run());
            var postItalics = para.AppendChild(new Run());

            var normalPreText = new Text
            {
                Text = txt[..italicStart],
                Space = SpaceProcessingModeValues.Preserve
            };

            var italicText = new Text
            {
                Text = txt[italicStart..italicEnd],
                Space = SpaceProcessingModeValues.Preserve
            };

            var normalPostText = new Text
            {
                Text = txt[italicEnd..],
                Space = SpaceProcessingModeValues.Preserve
            };

            preItalics.AppendChild(normalPreText);
            var runProperties = italics.AppendChild(new RunProperties(new Italic()));
            italics.AppendChild(italicText);
            postItalics.AppendChild(normalPostText);
        }
    }
}