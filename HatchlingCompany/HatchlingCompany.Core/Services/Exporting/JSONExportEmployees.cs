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
    public class JSONExportEmployees : ICommand
    {
        private readonly IDbContext db;
        private readonly ISerializer serializer;
        private readonly IExporter exporter;
        private readonly IWriter writer;

        public JSONExportEmployees(IDbContext db, ISerializer serializer, IExporter exporter, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.exporter = exporter ?? throw new ArgumentNullException(nameof(exporter));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count < 2)
            {
                throw new ArgumentNullException("Invalid parameters! Please type in JSONExportEmployees [All / Employee_Emails (separated by spaces)]");
            }

            if (String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace!");
            }

            var employeesToExport = GetEmployees(parameters.Skip(1).ToList());
             
            string jsonToExport = this.serializer.Serialize(employeesToExport);

            exporter.Export("json", jsonToExport);

            this.writer.WriteLine("Employees successfully exported!");
        }

        private IList<ListEmployeeDetailsModel> GetEmployees(IList<string> parameters)
        {
            if (parameters[0].Equals("all"))
            {
                return this.db.Employees.ProjectToList<ListEmployeeDetailsModel>();
            }
            else
            {
                return this.db.Employees.Where(e => parameters.Contains(e.Email)).ProjectToList<ListEmployeeDetailsModel>();
            }
        }
    }
}
