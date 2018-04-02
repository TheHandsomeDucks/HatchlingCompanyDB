using System.IO;
using HatchlingCompany.Utils.Contracts;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace HatchlingCompany.Utils
{
    public class PDFExporter
    {
        private string path;
        private string fileName;

        public PDFExporter()
        {
            this.path = "./../../../PDF/Exports/";
            this.fileName = "export.pdf";
        }

        public void Export(object obj)
        {
            var doc = this.CreateDocument(obj);
            this.DefineStyles(doc);
            this.AddHeader(doc);
            this.FillContent(doc, obj);
            var pdf = this.Render(doc);
            this.Save(pdf);
        }

        private Document CreateDocument(object obj)
        {
            var document = new Document();
            document.Info.Title = "Hatchling Report";
            document.Info.Subject = $"Hatchling report: {obj.GetType().Name}";
            document.Info.Author = "The Handsome Ducks";

            return document;
        }

        private void DefineStyles(Document doc)
        {
            doc.Styles[StyleNames.Normal].Font.Name = "Verdana";
            doc.Styles[StyleNames.Normal].Font.Size = 10;

            doc.Styles[StyleNames.Header].Font.Size = 16;
            doc.Styles[StyleNames.Header].ParagraphFormat.Alignment = ParagraphAlignment.Center;
        }

        private void AddHeader(Document doc)
        {
            var section = doc.AddSection();

            var paragraph = section.Headers.Primary.AddParagraph();

            var img = paragraph.AddImage("./../../../HatchlingCompany.Utils/Assets/duck.png");
            img.LockAspectRatio = true;
            img.Height = 100;
            img.RelativeVertical = RelativeVertical.Line;
            img.RelativeHorizontal = RelativeHorizontal.Column;
            img.Top = ShapePosition.Top;
            img.Left = ShapePosition.Left;

            paragraph.AddText("HATCHLING COMPANY REPORT");
        }

        private void FillContent(Document doc, object obj)
        {
            var section = doc.Sections[0];

            var paragraph = section.AddParagraph(obj.GetType().Name);
            paragraph.Format.SpaceBefore = 150;
            paragraph.Format.SpaceAfter = 10;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;

            foreach (var property in obj.GetType().GetProperties())
            {
                paragraph = section.AddParagraph();
                paragraph.Format.AddTabStop(100);
                paragraph.AddFormattedText(property.Name, TextFormat.Underline);
                paragraph.AddText(":");
                paragraph.AddTab();
                paragraph.AddFormattedText(property.GetValue(obj).ToString());
            }
        }

        private PdfDocument Render(Document doc)
        {
            var renderer = new PdfDocumentRenderer();
            renderer.Document = doc;
            renderer.RenderDocument();
            return renderer.PdfDocument;
        }

        private void Save(PdfDocument doc)
        {
            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path);
            }

            doc.Save(this.path + this.fileName);
        }
    }
}
