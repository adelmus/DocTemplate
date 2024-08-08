using System;
using System.Drawing;
using System.IO;
using DocTemplate;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;


namespace DocumentTemplate
{
    class GenerateDocument
    {
        Document document;
        
        public MemoryStream Generate(DocField docField)
        { 
            CreateBarCodePages cr = new CreateBarCodePages();
            
            Document document = new Document($"/files/{docField.FileName}", FileFormat.Auto);
            int pages = document.PageCount;
            int pageCount = 1;
            foreach (Section section in document.Sections)
            {                
                
                if (docField.IsQRcode)
                {
                    section.Paragraphs.Add(cr.GetQRCode(section, docField.code, pages, pageCount, docField.Position.Height, docField.Position.Width, docField.Position.HorizontalPosition, docField.Position.VerticalPosition));
                }
                if (docField.IsBarcode)
                {                    
                    section.Paragraphs.Add(cr.GetBarCode(section, docField.code, pages, pageCount, docField.Position.Height, docField.Position.Width, docField.Position.HorizontalPosition, docField.Position.VerticalPosition));                
                }
                pageCount++;
            }            

            ToPdfParameterList parameters = new ToPdfParameterList();
            //PrivateFontPath fontPath = new PrivateFontPath("c39hrp24dltt", "fonts/c39hrp24dltt.ttf");
            //parameters.PrivateFontPaths.Add(fontPath);

            document.MailMerge.Execute(docField.Fields.Keys.ToArray(), docField.Fields.Values.ToArray());  
            document.IsUpdateFields = true;
            MemoryStream stream = new MemoryStream();            
            document.SaveToStream(stream, parameters);
            document.Close();            
            return stream;
        }

        public string[] List(string fileName)
        {
            Document document = new Document(fileName, FileFormat.Auto);
            string[] GroupNames = document.MailMerge.GetMergeFieldNames();
            
            return GroupNames;
        }
    }
}