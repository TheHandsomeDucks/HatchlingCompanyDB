using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class FindEmployeeByMail : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public FindEmployeeByMail(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count() < 2)
            {
                throw new ArgumentException("Invalid parameters! PLease use findEmployeeByMail [email]");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentException("Email cannot be null, empty or whitespace");
            }

            var email = parameters[1];

            var employeeExists = this.db.Employees.SingleOrDefault(e => e.Email == email);

            if (employeeExists == null)
            {
                throw new ArgumentNullException($"Person with {email} could not be found");
            }

            var employee = this.db
                             .Employees
                             .Where(e => e.Email == email)
                             .ProjectTo<ListEmployeeDetailsModel>()
                             .SingleOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine("Listing employees details...");
            sb.AppendLine(employee.PrintInfo());
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine($"All details for employee {employee.FirstName} {employee.LastName} have been listed");
        }
    }
}
