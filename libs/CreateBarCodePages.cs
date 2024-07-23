using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc;
using System.Drawing;
using Spire.Barcode;

namespace DocumentTemplate
{
    public class CreateBarCodePages
    {
        public void Run(string code, int totalPages)
        {
            // Instantiate document object
            Document document = new Document();

            for (int page=0; page < totalPages; page++) {
                //Add a section 
                Section section = document.AddSection();

                //Set the margin
                section.PageSetup.Margins.Left = 50;
                section.PageSetup.Margins.Right = 50;
                Paragraph paragraph = section.AddParagraph();

                //Add texbox 1
                TextBox textBox1 = paragraph.AppendTextBox(section.PageSetup.Margins.Left - 20, section.PageSetup.PageSize.Height + 20);

                //Fix the position of textbox 
                textBox1.Format.HorizontalOrigin = HorizontalOrigin.Page;
                textBox1.Format.HorizontalPosition = 0;
                textBox1.Format.VerticalPosition = 200f;
                textBox1.Format.VerticalOrigin = VerticalOrigin.Page;

                //Set the text vertically
                textBox1.Format.TextAnchor = ShapeVerticalAlignment.Center;
                textBox1.Format.LayoutFlowAlt = TextDirection.LeftToRight;
                textBox1.Format.FillColor = Color.Transparent;
                textBox1.Format.LineColor = Color.Transparent;

                //Add text and set the font
                Paragraph textboxPara1 = textBox1.Body.AddParagraph();
                TextRange txtrg = textboxPara1.AppendText($"*{code}{totalPages:00}{page+1:00}*");
                txtrg.CharacterFormat.FontName = "c39hrp24dltt";
                txtrg.CharacterFormat.FontSize = 15;
                txtrg.CharacterFormat.TextColor = System.Drawing.Color.Black;
                textboxPara1.Format.HorizontalAlignment = HorizontalAlignment.Center;
            }

            //Save the document
            document.SaveToFile("Result.docx");

        }

        public Paragraph GetBarCode(Section section, string code, int totalPages, int Page)
        {
            Paragraph paragraph = section.AddParagraph();
            //Paragraph paragraph = new Paragraph(doc);
            
            //Add texbox 1
            //TextBox textBox1 = paragraph.AppendTextBox(section.PageSetup.Margins.Left - 20, section.PageSetup.PageSize.Height + 20);
            TextBox textBox1 = paragraph.AppendTextBox(section.PageSetup.Margins.Left, section.PageSetup.PageSize.Height + 20);

            //Fix the position of textbox 
            textBox1.Format.HorizontalOrigin = HorizontalOrigin.Page;
            textBox1.Format.HorizontalPosition = 0;
            textBox1.Format.VerticalPosition = 200f;
            textBox1.Format.VerticalOrigin = VerticalOrigin.Page;

            //Set the text vertically
            textBox1.Format.TextAnchor = ShapeVerticalAlignment.Center;
            textBox1.Format.LayoutFlowAlt = TextDirection.LeftToRight;
            textBox1.Format.FillColor = Color.Transparent;
            textBox1.Format.LineColor = Color.Transparent;

            //Add text and set the font
            Paragraph textboxPara1 = textBox1.Body.AddParagraph();
            TextRange txtrg = textboxPara1.AppendText($"*{code}{totalPages:00}{Page:00}*");
            txtrg.CharacterFormat.FontName = "c39hrp24dltt";
            txtrg.CharacterFormat.FontSize = 15;
            txtrg.CharacterFormat.TextColor = Color.Black;
            textboxPara1.Format.HorizontalAlignment = HorizontalAlignment.Center;

            //paragraph.ApplyStyle("StyleBarCode");
            return paragraph;
        }


        public Image GetQRcodeImage(string code)
        {
            BarcodeSettings settings = new BarcodeSettings();
            settings.Type = BarCodeType.QRCode;

            settings.Data = code;
            settings.Data2D = code;
            settings.QRCodeDataMode = QRCodeDataMode.AlphaNumber;
            settings.X = 1.0f;
            settings.ShowText = false;
            settings.QRCodeECL = QRCodeECL.H;

            BarCodeGenerator generator = new BarCodeGenerator(settings);
            //Image image = generator.GenerateImage();
            //image.Save("QRCode.png");
            return generator.GenerateImage();
        }


        public Paragraph GetQRCode(Section section, string code, int totalPages, int Page)
        {
            Paragraph paragraph = section.AddParagraph();

            
            //Paragraph paragraph = new Paragraph(doc);

            //Append a Textbox to paragraph
            TextBox tb = paragraph.AppendTextBox(45, 40);

            //Set the position of Textbox
            tb.Format.HorizontalOrigin = HorizontalOrigin.Page;
            tb.Format.HorizontalPosition = 4;
            tb.Format.VerticalOrigin = VerticalOrigin.Page;
            //tb.Format.VerticalPosition = section.PageSetup.PageSize.Height - 40f;
            tb.Format.VerticalPosition = 60f;
            tb.Format.LineColor = Color.Transparent;
            tb.Format.FillColor = Color.Transparent;
            //tb.Format.LayoutFlowAlt = TextDirection.LeftToRight;

            //Set the fill effect of Textbox as picture
            tb.Format.FillEfects.Type = BackgroundType.Picture;

            //Fill the Textbox with a picture
            tb.Format.FillEfects.Picture = GetQRcodeImage($"{code}{totalPages:00}{Page:00}:LT");
            return paragraph;
        }
    }
}
