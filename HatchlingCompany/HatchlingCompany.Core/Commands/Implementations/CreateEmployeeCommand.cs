using HatchlingCompany.Core.Contracts;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Commands.Implementations
{
    public class CreateEmployeeCommand : Command, ICommand
    {
        private readonly IHatchlingCompanyDbContext db;

        public CreateEmployeeCommand(IHatchlingCompanyDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public override void Execute()
        {
            var parameters = this.Parameters;
            var email = parameters[1];
            var phoneNumber = parameters[2];
            //var role = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), parameters[5].ToLower());

            var employeeFound = this.db.Employees.SingleOrDefault(e => e.PhoneNumber == phoneNumber);

            if (employeeFound != null)
            {
                throw new ArgumentNullException($"{employeeFound.FirstName} {employeeFound.LastName} already exists");
            }

            this.db.Employees.Add(new Employee
            {
                Email = email
            });
            this.db.SaveChanges();
        }
    }
}
