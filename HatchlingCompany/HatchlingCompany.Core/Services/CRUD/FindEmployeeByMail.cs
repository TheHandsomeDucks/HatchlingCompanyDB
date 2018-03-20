using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var email = parameters[1];

            var employee = this.db.Employees.SingleOrDefault(e => e.Email == email);

            if (employee == null)
            {
                throw new ArgumentNullException($"Person with {email} could not be found");
            }

            this.writer.WriteLine($"Fullname: {employee.FirstName} {employee.LastName}");
            this.writer.WriteLine($"Email: {employee.Email}");
            this.writer.WriteLine($"Phone: {employee.PhoneNumber}");

            this.writer.WriteLine($"Employee with email {email} was found");
        }
    }
}
