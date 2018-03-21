using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using HatchlingCompany.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Core.Services.Listing
{
    public class ListEmployeesByStatus : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListEmployeesByStatus(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), parameters[1].ToLower());

            var employees = this.db
                                .Employees
                                .Where(e => e.Status == status)
                                .ProjectTo<ListEmployeesModel>()
                                .ToList();

            if (!employees.Any())
            {
                throw new ArgumentNullException($"Employees with {status} could not be found");
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Listing employees with status {status}...");
            employees.ForEach(e => sb.AppendLine($"Name: {e.FirstName} {e.LastName}"));
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine($"All employees with status {status} have been listed");
        }
    }
}
