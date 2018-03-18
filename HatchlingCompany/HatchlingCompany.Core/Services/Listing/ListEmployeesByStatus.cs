using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data;
using HatchlingCompany.Models.Common;
using System;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Core.Services.Listing
{
    public class ListEmployeesByStatus : Command
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListEmployeesByStatus(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public override void Execute()
        {
            var parameters = this.Parameters;
            var status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), parameters[1].ToLower());

            var employees = this.db
                                .Employees
                                .Where(e => e.Status == status)
                                .ToList();

            if (!employees.Any())
            {
                throw new ArgumentNullException($"Employees with {status} could not be found");
            }

            var sb = new StringBuilder();

            employees.ForEach(e => sb.AppendLine($"FullName: {e.FirstName} {e.LastName}"));

            this.writer.WriteLine(sb.ToString());
        }
    }
}
