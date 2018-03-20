using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Data;
using HatchlingCompany.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class UpdateEmployeeStatus : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public UpdateEmployeeStatus(IDbContext db, IWriter writer)
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

            var email = parameters[1];
            var status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), parameters[2].ToLower());

            var employee = this.db.Employees
                                .Where(e => e.Email == email)
                                .SingleOrDefault();

            if (employee == null)
            {
                throw new ArgumentNullException($"Person with {email} could not be found");
            }

            employee.Status = status;

            this.db.SaveChanges();

            this.writer.WriteLine($"{employee.FirstName} {employee.LastName} status was updated to {employee.Status}");
        }
    }
}
