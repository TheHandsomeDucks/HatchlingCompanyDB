using System.IO;
using HatchlingCompany.Utils.Contracts;

namespace HatchlingCompany.Utils
{
    public class FileExporter : IExporter
    {
        public void Export(string fileType, string text)
        {
            var path = $"./../../../{fileType.ToUpper()}/Exports/";
            var fileName = $"export.{fileType.ToLower()}";
            var file = path + fileName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter writer = File.CreateText(file))
            {
                writer.WriteLine(text);
            }
        }
    }
}
