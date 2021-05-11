using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace RefCatalogue.Controllers
{
    internal class Exporter
    {
        public static void ExportToWord(string[,] referenceList)
        {
            // Open a WordprocessingDocument for editing using the file path.
            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(@"C:\Projects\RefCatalogue\Reference.docx", true);
            // Assign a reference to the existing document body.
            Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

            for (int i = 0; i <= referenceList.GetLength(0) - 1; i++)
            {
                Paragraph para = body.AppendChild(new Paragraph());
                string reference = referenceList[i, 1];
                if (referenceList[i, 0] == "Book")
                {
                    // find the index to start italics - the closing bracket on the year attribute
                    int italicStart = reference.IndexOf(')') + 2;

                    // find the index to end the italics - 1st fullstop after the start index
                    int italicEnd = reference.IndexOf('.', italicStart) + 1;
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "Journal")
                {
                    // find the index to start italics - the 1st apostrophe/comma combo (',)
                    int italicStart = referenceList[i, 1].IndexOf("',") + 2;

                    // find the index to end the italics - 1st comma after the start index
                    int italicEnd = referenceList[i, 1].IndexOf(',', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "Conf Paper")
                {
                    // find the index to start italics - the 1st apostrophe/comma combo (',)
                    int italicStart = referenceList[i, 1].IndexOf("',") + 2;

                    // find the index to end the italics - 1st fullstop after the start index
                    int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "Website")
                {
                    // find the index to start italics - the closing bracket on the year attribute
                    int italicStart = referenceList[i, 1].IndexOf(')') + 1;

                    // find the index to end the italics - 1st fullstop after the start index
                    int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "Blog")
                {
                    // find the index to start italics - the 1st apostrophe/comma combo (',)
                    int italicStart = referenceList[i, 1].IndexOf("',") + 2;

                    // find the index to end the italics - 1st fullstop after the start index
                    int italicEnd = referenceList[i, 1].IndexOf(',', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else if (referenceList[i, 0] == "RFC")
                {
                    // find the index to start italics - the closing bracket on the year attribute
                    int italicStart = referenceList[i, 1].IndexOf(')') + 1;

                    // find the index to end the italics - 1st fullstop after the start index
                    int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);
                    AddRuns(italicStart, italicEnd, reference, i, para);
                }
                else
                {
                    Run run = para.AppendChild(new Run());
                    Text txt = new Text
                    {
                        Text = referenceList[i, 1],
                        Space = SpaceProcessingModeValues.Preserve
                    };
                    run.AppendChild(txt);
                }
            }

            wordprocessingDocument.Close();
        }

        public static void AddRuns(int italicStart, int italicEnd, string txt, int i, Paragraph para)
        {
            Run preItalics = para.AppendChild(new Run());
            Run italics = para.AppendChild(new Run());
            Run postItalics = para.AppendChild(new Run());

            Text normalPreText = new Text
            {
                Text = txt[0..italicStart],
                Space = SpaceProcessingModeValues.Preserve
            };

            Text italicText = new Text
            {
                Text = txt[italicStart..italicEnd],
                Space = SpaceProcessingModeValues.Preserve
            };

            Text normalPostText = new Text
            {
                Text = txt[italicEnd..],
                Space = SpaceProcessingModeValues.Preserve
            };

            preItalics.AppendChild(normalPreText);
            RunProperties runProperties = italics.AppendChild(new RunProperties(new Italic()));
            italics.AppendChild(italicText);
            postItalics.AppendChild(normalPostText);
        }
    }
}