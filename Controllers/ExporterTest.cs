using System.Data;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using DocumentFormat.OpenXml;

namespace RefCatalogue.Controllers
{
    internal class ExporterTest
    {
        
        // Open a WordprocessingDocument for editing using the filepath.
        private static WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(@"C:\Projects\RefCatalogue\Reference.docx", true);
        // Assign a reference to the existing document body.
        private static Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
        private static Paragraph para = body.AppendChild(new Paragraph());

        public static void ExportToWord(string[,] referenceList)
        {

            for (int i = 0; i <= referenceList.GetLength(0) - 1; i++)
            {
                if (referenceList[i, 0] == "Book")
                {
                    string txt = referenceList[i, 1];

                    // find the index to start italics - the closing bracket on the year attribute
                    int italicStart = txt.IndexOf(')') + 2;

                    // find the index to end the italics - 1st fullstop after the start index
                    int italicEnd = txt.IndexOf('.', italicStart) + 1;
                    AddRuns(italicStart, italicEnd, txt, i);
                }
                else if (referenceList[i, 0] == "Journal")
                {
                    ExportJournals(referenceList, i);
                }
                else if (referenceList[i, 0] == "Conf Paper")
                {
                    ExportConfPapers(referenceList, i);
                }
                else if (referenceList[i, 0] == "Website")
                {
                    ExportWebsite(referenceList, i);
                }
                else if (referenceList[i, 0] == "Blog")
                {
                    ExportBlog(referenceList, i);
                }
                else if (referenceList[i, 0] == "RFC")
                {
                    ExportRFC(referenceList, i);
                }
                else
                {
                    Paragraph para = body.AppendChild(new Paragraph());
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

        private static void ExportJournals(string[,] referenceList, int i)
        {
            

            Run preItalics = para.AppendChild(new Run());
            Run italics = para.AppendChild(new Run());
            Run postItalics = para.AppendChild(new Run());
            string txt = referenceList[i, 1];

            // find the index to start italics - the 1st apostrophe/comma combo (',)
            int italicStart = referenceList[i, 1].IndexOf("',") + 2;

            // find the index to end the italics - 1st comma after the start index
            int italicEnd = referenceList[i, 1].IndexOf(',', italicStart);

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

        private static void ExportConfPapers(string[,] referenceList, int i)
        {
            

            Run preItalics = para.AppendChild(new Run());
            Run italics = para.AppendChild(new Run());
            Run postItalics = para.AppendChild(new Run());
            string txt = referenceList[i, 1];

            // find the index to start italics - the 1st apostrophe/comma combo (',)
            int italicStart = referenceList[i, 1].IndexOf("',") + 2;

            // find the index to end the italics - 1st fullstop after the start index
            int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);

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

        private static void ExportWebsite(string[,] referenceList, int i)
        {
            

            Run preItalics = para.AppendChild(new Run());
            Run italics = para.AppendChild(new Run());
            Run postItalics = para.AppendChild(new Run());
            string txt = referenceList[i, 1];

            // find the index to start italics - the closing bracket on the year attribute
            int italicStart = referenceList[i, 1].IndexOf(')') + 1;

            // find the index to end the italics - 1st fullstop after the start index
            int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);

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

        private static void ExportBlog(string[,] referenceList, int i)
        {
            

            Run preItalics = para.AppendChild(new Run());
            Run italics = para.AppendChild(new Run());
            Run postItalics = para.AppendChild(new Run());
            string txt = referenceList[i, 1];

            // find the index to start italics - the 1st apostrophe/comma combo (',)
            int italicStart = referenceList[i, 1].IndexOf("',") + 2;

            // find the index to end the italics - 1st fullstop after the start index
            int italicEnd = referenceList[i, 1].IndexOf(',', italicStart);

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

        private static void ExportRFC(string[,] referenceList, int i)
        {
            

            Run preItalics = para.AppendChild(new Run());
            Run italics = para.AppendChild(new Run());
            Run postItalics = para.AppendChild(new Run());
            string txt = referenceList[i, 1];

            // find the index to start italics - the closing bracket on the year attribute
            int italicStart = referenceList[i, 1].IndexOf(')') + 1;

            // find the index to end the italics - 1st fullstop after the start index
            int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);

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

        public static void AddRuns(int italicStart, int italicEnd, string txt, int i)
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