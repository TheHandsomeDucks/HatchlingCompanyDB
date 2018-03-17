﻿using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class FindEmployeeByMail : Command
    {
        private readonly IHatchlingCompanyDbContext db;
        private readonly IWriter writer;

        public FindEmployeeByMail(IHatchlingCompanyDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public override void Execute()
        {
            var parameters = this.Parameters;
            var email = parameters[1];

            var employee = this.db.Employees.SingleOrDefault(e => e.Email == email);

            if (employee == null)
            {
                this.writer.WriteLine($"Person with {email} could not be found");
            }
            else
            {
                this.writer.WriteLine($"Fullname:{employee.FirstName} {employee.LastName}");
                this.writer.WriteLine($"Email:{employee.Email}");
                this.writer.WriteLine($"Phone:{employee.PhoneNumber}");
            }

        }
    }
}
