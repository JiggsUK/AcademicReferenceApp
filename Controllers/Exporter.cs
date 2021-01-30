//using System.Data;
//using System.Linq;
//using Word = Microsoft.Office.Interop.Word;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;
//using System.Collections.Generic;

//namespace RefCatalogue.Controllers
//{
//    internal class Exporter
//    {
//        private Paragraph para { get; set; }
//        List<Run> refToExport = new List<Run>();

//        public Exporter()
//        {
//            // Open a WordprocessingDocument for editing using the filepath.
//            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open("Reference.docx", true);
//            // Assign a reference to the existing document body.
//            Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
            
            
//        }

//        public void ExportToWord(string[,] referenceList)
//        {
//            int prevParagraphsTotalLength = 0;
//            object missingValue = System.Reflection.Missing.Value;
//            Word._Application wordApp;
//            Word._Document document;
//            wordApp = new Word.Application
//            {
//                Visible = true
//            };
//            document = wordApp.Documents.Add(ref missingValue, ref missingValue, ref missingValue, ref missingValue);

//            for (int i = 0; i <= referenceList.GetLength(0) - 1; i++)
//            {
//                if (referenceList[i, 0] == "Book")
//                {
//                    ExportBooks(referenceList, prevParagraphsTotalLength, missingValue, document, i);
//                }
//                else if (referenceList[i, 0] == "Journal")
//                {
//                    ExportJournals(referenceList, prevParagraphsTotalLength, missingValue, document, i);
//                }
//                else if (referenceList[i, 0] == "Conf Paper")
//                {
//                    ExportConfPapers(referenceList, prevParagraphsTotalLength, missingValue, document, i);
//                }
//                else if (referenceList[i, 0] == "Website")
//                {
//                    ExportWebsite(referenceList, prevParagraphsTotalLength, missingValue, document, i);
//                }
//                else if (referenceList[i, 0] == "Blog")
//                {
//                    ExportBlog(referenceList, prevParagraphsTotalLength, missingValue, document, i);
//                }
//                else if (referenceList[i, 0] == "RFC")
//                {
//                    ExportRFC(referenceList, prevParagraphsTotalLength, missingValue, document, i);
//                }
//                else
//                {
//                    Run run = para.AppendChild(new Run());
//                    var txt = referenceList[i, 1];
//                    run.AppendChild(new Text(txt));
//                    refToExport.Add(run);

//                    Word.Paragraph paragraph;
//                    paragraph = document.Content.Paragraphs.Add(ref missingValue);
//                    paragraph.Range.Text = referenceList[i, 1];
//                    paragraph.Range.InsertParagraphAfter();
//                }

//                prevParagraphsTotalLength = (referenceList[i, 1].Length + 1) + prevParagraphsTotalLength;
//            }
//        }

//        private void ExportBooks(string[,] referenceList, int prevParagraphsTotalLength, object missingValue, Word._Document document, int i)
//        {
//            Run run = para.AppendChild(new Run());
//            string txt = referenceList[i, 1];

//            // find the index to start italics - the closing bracket on the year attribute
//            int italicStart = txt.IndexOf(')') + 1;

//            // find the index to end the italics - 1st fullstop after the start index
//            int italicEnd = txt.IndexOf('.', italicStart);

//            string normalPreText = txt[0..(italicStart - 1)];
//            string italicText = txt[italicStart..(italicEnd - italicStart)];
//            string normalPostText = txt[italicEnd..];

//            run.AppendChild(new Text(normalPreText));
//            run.AppendChild(new Text(normalPostText));

//            RunProperties runProperties = run.PrependChild(new RunProperties(new Italic()));
//            run.PrependChild(new Text(italicText));
//            refToExport.Add(run);

//            Word.Paragraph paragraph;
//            paragraph = document.Content.Paragraphs.Add(ref missingValue);
//            paragraph.Range.Text = referenceList[i, 1];

//            // select the relevant text and italicise it
//            Word.Range textToItalicse = document.Range(italicStart + prevParagraphsTotalLength, italicEnd + prevParagraphsTotalLength);
//            textToItalicse.Select();
//            textToItalicse.Font.Italic = 1;
//            paragraph.Range.InsertParagraphAfter();
//        }

//        private static void ExportJournals(string[,] referenceList, int prevParagraphsTotalLength, object missingValue, Word._Document document, int i)
//        {
//            // find the index to start italics - the 1st apostrophe/comma combo (',)
//            int italicStart = referenceList[i, 1].IndexOf("',") + 2;

//            // find the index to end the italics - 1st comma after the start index
//            int italicEnd = referenceList[i, 1].IndexOf(',', italicStart);

//            Word.Paragraph paragraph;
//            paragraph = document.Content.Paragraphs.Add(ref missingValue);
//            paragraph.Range.Text = referenceList[i, 1];
//            Word.Range italics = document.Range(italicStart + prevParagraphsTotalLength, italicEnd + prevParagraphsTotalLength);
//            italics.Select();
//            italics.Font.Italic = 1;
//            paragraph.Range.InsertParagraphAfter();
//        }

//        private static void ExportConfPapers(string[,] referenceList, int prevParagraphsTotalLength, object missingValue, Word._Document document, int i)
//        {
//            // find the index to start italics - the 1st apostrophe/comma combo (',)
//            int italicStart = referenceList[i, 1].IndexOf("',") + 2;

//            // find the index to end the italics - 1st fullstop after the start index
//            int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);

//            Word.Paragraph paragraph;
//            paragraph = document.Content.Paragraphs.Add(ref missingValue);
//            paragraph.Range.Text = referenceList[i, 1];
//            Word.Range italics = document.Range(italicStart + prevParagraphsTotalLength, italicEnd + prevParagraphsTotalLength);
//            italics.Select();
//            italics.Font.Italic = 1;
//            paragraph.Range.InsertParagraphAfter();
//        }

//        private static void ExportWebsite(string[,] referenceList, int prevParagraphsTotalLength, object missingValue, Word._Document document, int i)
//        {
//            // find the index to start italics - the closing bracket on the year attribute
//            int italicStart = referenceList[i, 1].IndexOf(')') + 1;

//            // find the index to end the italics - 1st fullstop after the start index
//            int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);

//            Word.Paragraph paragraph;
//            paragraph = document.Content.Paragraphs.Add(ref missingValue);
//            paragraph.Range.Text = referenceList[i, 1];
//            Word.Range italics = document.Range(italicStart + prevParagraphsTotalLength, italicEnd + prevParagraphsTotalLength);
//            italics.Select();
//            italics.Font.Italic = 1;
//            paragraph.Range.InsertParagraphAfter();
//        }

//        private static void ExportBlog(string[,] referenceList, int prevParagraphsTotalLength, object missingValue, Word._Document document, int i)
//        {
//            // find the index to start italics - the 1st apostrophe/comma combo (',)
//            int italicStart = referenceList[i, 1].IndexOf("',") + 2;

//            // find the index to end the italics - 1st fullstop after the start index
//            int italicEnd = referenceList[i, 1].IndexOf(',', italicStart);

//            Word.Paragraph paragraph;
//            paragraph = document.Content.Paragraphs.Add(ref missingValue);
//            paragraph.Range.Text = referenceList[i, 1];
//            Word.Range italics = document.Range(italicStart + prevParagraphsTotalLength, italicEnd + prevParagraphsTotalLength);
//            italics.Select();
//            italics.Font.Italic = 1;
//            paragraph.Range.InsertParagraphAfter();
//        }

//        private static void ExportRFC(string[,] referenceList, int prevParagraphsTotalLength, object missingValue, Word._Document document, int i)
//        {
//            // find the index to start italics - the closing bracket on the year attribute
//            int italicStart = referenceList[i, 1].IndexOf(')') + 1;

//            // find the index to end the italics - 1st fullstop after the start index
//            int italicEnd = referenceList[i, 1].IndexOf('.', italicStart);

//            Word.Paragraph paragraph;
//            paragraph = document.Content.Paragraphs.Add(ref missingValue);
//            paragraph.Range.Text = referenceList[i, 1];
//            Word.Range italics = document.Range(italicStart + prevParagraphsTotalLength, italicEnd + prevParagraphsTotalLength);
//            italics.Select();
//            italics.Font.Italic = 1;
//            paragraph.Range.InsertParagraphAfter();
//        }
//    }
//}