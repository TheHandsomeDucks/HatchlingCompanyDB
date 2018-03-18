using HatchlingCompany.Utils.Contracts;
using Newtonsoft.Json;
using System;
using System.IO;

namespace HatchlingCompany.Utils
{
    public class JSONImporter : IImporter
    {
        private string path;
        private string fileName;

        public JSONImporter()
        {
            this.path = "./../../../JSON/Imports/";
            this.fileName = "import.json";
        }

        public object Import()
        {
            return Deserialize(this.path, this.fileName);
        }

        public object Import(string path, string fileName)
        {
            return Deserialize(path, fileName);
        }

        private object Deserialize(string path, string fileName)
        {
            string file = String.Concat(path, fileName);

            if (!Directory.Exists(path))
            {
                throw new ArgumentException("Directory doesn't exist");
            }

            if (!File.Exists(file))
            {
                throw new ArgumentException("File doesn't exist");
            }

            string serialized = File.ReadAllText(file);

            object obj = JsonConvert.DeserializeObject<object>(serialized);

            return obj;
        }
    }
}
