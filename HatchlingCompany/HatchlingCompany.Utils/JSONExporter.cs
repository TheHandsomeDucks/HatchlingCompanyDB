using HatchlingCompany.Utils.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
            string serialized = Serialize(obj);

            SaveToFile(serialized, this.path, this.fileName);
        }

        public void Export(object obj, string path, string fileName)
        {
            string serialized = Serialize(obj);

            SaveToFile(serialized, path, fileName);
        }

        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private void SaveToFile(string text, string path, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string file = String.Concat(path, fileName);

            using (StreamWriter writer = File.CreateText(file))
            {
                writer.WriteLine(text);
            }
        }
    }
}
