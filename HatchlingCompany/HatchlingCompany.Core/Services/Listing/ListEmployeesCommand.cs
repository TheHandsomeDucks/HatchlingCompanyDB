using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using System;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Core.Services.Listing
{
    public class ListEmployeesCommand : Command
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListEmployees(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public override void Execute()
        {
            var employees = this.db
                            .Employees
                            .ProjectTo<ListEmployeesModel>()
                            .ToList();

            if (!employees.Any())
            {
                this.writer.WriteLine("No employees registered");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("Listing employees...");

            foreach (var employee in employees)
            {
                sb.AppendLine(employee.PrintInfo());
            }

            this.writer.WriteLine(sb.ToString());

        }
    }
}
