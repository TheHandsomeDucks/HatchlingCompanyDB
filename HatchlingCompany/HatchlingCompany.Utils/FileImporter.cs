using System.IO;
using HatchlingCompany.Utils.Contracts;

namespace HatchlingCompany.Utils
{
    public class FileImporter : IImporter
    {
        public string Import(string fileType)
        {
            var path = $"./../../../{fileType.ToUpper()}/Imports/";
            var fileName = $"import.{fileType.ToLower()}";
            var file = path + fileName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamReader reader = new StreamReader(file))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
