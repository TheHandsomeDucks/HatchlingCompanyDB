using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.Core.Commands.Implementations
{
    public class CreateEmployee : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public CreateEmployee(IDbContext db, IWriter writer)
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

            var firstName = parameters[1];
            var lastName = parameters[2];
            var email = parameters[3];
            var phoneNumber = parameters[4];

            var employee = this.db.Employees.SingleOrDefault(e => e.Email == email);

            if (employee != null)
            {
                throw new ArgumentNullException($"{employee.FirstName} {employee.LastName} already exists");
            }


            this.db.Employees.Add(new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            });

            this.db.SaveChanges();

            this.writer.WriteLine($"A new employee with name {firstName} {lastName} was created.");
        }
    }
}
