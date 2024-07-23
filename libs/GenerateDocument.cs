using System;
using System.Drawing;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;


namespace DocumentTemplate
{
    class GenerateDocument
    {
        Document document;
        //public GenerateDocument(Document _document) {
        //public void GenerateDocument() {
        //    Document document = new Document("test.docx", FileFormat.Auto);
        //}

        public MemoryStream Generate(string fileName, string code, string[] fields, string[] values)
        { 
            CreateBarCodePages cr = new CreateBarCodePages();
            
            Document document = new Document(fileName, FileFormat.Auto);
            int pages = document.PageCount;
            

            foreach (Section section in document.Sections)
            {
                //section.Paragraphs.Add(cr.GetBarCode(section, "IN008978DOCXXXX", 1, 1));
                section.Paragraphs.Add(cr.GetQRCode(section, code, 1, 1));
            }

            ToPdfParameterList parameters = new ToPdfParameterList();
            PrivateFontPath fontPath = new PrivateFontPath("c39hrp24dltt", "fonts/c39hrp24dltt.ttf");
            parameters.PrivateFontPaths.Add(fontPath);

            document.MailMerge.Execute(fields, values);           
            MemoryStream stream = new MemoryStream();
            //document.SaveToStream(stream, FileFormat.Doc);
            document.SaveToFile("/files/test1.pdf", parameters);
            document.SaveToStream(stream, parameters);
            document.Close();
            document = new Document(stream, FileFormat.Auto);
            //document.SaveToStream(stream, FileFormat.PDF);
            //document.SaveToStream(stream, FileFormat.PDF);
            //document.SaveToFile("demo.docx", FileFormat.Docx2019);
            //document.SaveToFile("/app/test1.pdf", FileFormat.PDF);
            return stream;
        }

        public string[] List()
        {
            Document document = new Document("files/test.docx", FileFormat.Auto);
            string[] GroupNames = document.MailMerge.GetMergeFieldNames();
            //Console.WriteLine("------------------ Group --------------------");
            //foreach (string field in GroupNames)
            //{
            //    Console.WriteLine(field);
            //}

            return GroupNames;
        }
    }
}