using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using HatchlingCompany.Utils.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.Core.Services.Exporting
{
    public class JSONImportEmployees : ICommand
    {
        private readonly IDbContext db;
        private readonly IDeserializer<IList<Employee>> deserializer;
        private readonly IImporter importer;
        private readonly IWriter writer;

        public JSONImportEmployees(IDbContext db, IDeserializer<IList<Employee>> deserializer, IImporter importer, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
            this.importer = importer ?? throw new ArgumentNullException(nameof(importer));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count != 1)
            {
                throw new ArgumentNullException("Invalid parameters! Please type in JSONImportEmployees");
            }

            if (String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace!");
            }

            var serializedEmployees = this.importer.Import("json");

            var deserializedEmployees = this.deserializer.Deserialize(serializedEmployees);

            ImportEmployees(deserializedEmployees);

            this.writer.Write("Employees successfully imported!");
        }

        private void ImportEmployees(IList<Employee> employees)
        {
            if (employees.Count == 0)
            {
                throw new ArgumentException("No employees to import");
            }

            foreach (Employee employee in employees)
            {
                this.db.Employees.Add(employee);
            }

            this.db.SaveChanges();
        }
    }
}
