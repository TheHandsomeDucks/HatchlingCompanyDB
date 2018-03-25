using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Core.Services.Listing
{
    public class ListEmployees : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListEmployees(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count != 1 || String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace");
            }

            var employees = this.db
                            .Employees
                            .ProjectTo<ListEmployeesModel>()
                            .ToList();

            if (!employees.Any())
            {
                throw new ArgumentException("No employees registered yet");
            }

            var sb = new StringBuilder();
            sb.AppendLine("Listing all employees...");
            employees.ForEach(e => sb.AppendLine($"{e.PrintInfo()}"));
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine($"All employees have been listed");
        }
    }
}
