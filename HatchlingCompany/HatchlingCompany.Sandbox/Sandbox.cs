using HatchlingCompany.Utils;

namespace HatchlingCompany.Sandbox
{
    internal class Sandbox
    {
        private static void Main()
        {
            var obj = new Human() { Name = "Pesho", Age = 10 };

            //var obj = new { Ivan = "Pesho", Bira = 0.5, Nazdrave = "Ayde nazdrave" };
            var pdfExporter = new PDFExporter();
            pdfExporter.Export(obj);
        }
    }

    internal class Human
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
