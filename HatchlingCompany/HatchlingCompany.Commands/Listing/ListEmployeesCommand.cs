using AutoMapper.QueryableExtensions;
using HatchlingCompany.Console.Commands.CRUD;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Commands.Listing
{
    public class ListEmployeesCommand : Command, ICommand
    {
        private readonly IHatchlingCompanyDbContext db;
        private readonly IWriter writer;

        public ListEmployeesCommand(IHatchlingCompanyDbContext db, IWriter writer)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new System.ArgumentNullException(nameof(writer));
        }

        public override void Execute()
        {
            var employees = this.db
                            .Employees
                            .ProjectTo<ListEmployees>()
                            .ToList();

            if (!employees.Any())
            {
                this.writer.WriteLine("No employees registered");
            }

            else
            {
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
}
