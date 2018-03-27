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
            if (parameters == null || parameters.Count != 2)
            {
                throw new ArgumentNullException("Parameters are invalid");
            }

            if (String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace");
            }

            var status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), parameters[1].ToLower());


            if (status == 0)
            {
                throw new ArgumentException("Status does not exists");
            }

            var employees = this.db
                                .Employees
                                .Where(e => e.Status == status)
                                .ProjectTo<ListEmployeesByStatusModel>()
                                .ToList();

            if (!employees.Any())
            {
                throw new ArgumentException($"Employees with status {status} could not be found");
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Listing employees with status {status}...");
            employees.ForEach(e => sb.AppendLine($"{e.PrintInfo()}"));
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine($"All employees with status {status} have been listed");
        }
    }
}
