using HatchlingCompany.Utils.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatchlingCompany.Utils
{
    public class JSONExporter : IExporter
    {
        private string path;
        private string fileName;

        public JSONExporter()
        {
            this.path = "./../../../JSON/Exports/";
            this.fileName = "export.json";
        }

        public void Export(object obj)
        {
            string serialized = JsonConvert.SerializeObject(obj);

            SaveToFile(serialized, this.path, this.fileName);
        }

        public void Export(object obj, string path, string fileName)
        {
            string serialized = JsonConvert.SerializeObject(obj);

            SaveToFile(serialized, path, fileName);
        }

        private void SaveToFile(string text, string path, string fileName)
        {
            string file = String.Concat(path, fileName);

            using (StreamWriter writer = File.CreateText(file))
            {
                writer.WriteLine(text);
            }
        }
    }
}
