using System.IO;
using HatchlingCompany.Utils.Contracts;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace HatchlingCompany.Utils
{
    public class PDFExporter : IExporter
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
            var document = new Document();
            var section = document.AddSection();

            foreach (var property in obj.GetType().GetProperties())
            {
                var paragraph = section.AddParagraph();
                paragraph.AddFormattedText($"{property.Name}:", TextFormat.Underline);
                paragraph.AddTab();
                paragraph.AddFormattedText(property.GetValue(obj).ToString());
            }

            var renderer = new PdfDocumentRenderer();
            renderer.Document = document;
            renderer.RenderDocument();

            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path);
            }

            renderer.PdfDocument.Save(this.path + this.fileName);
        }
    }
}
