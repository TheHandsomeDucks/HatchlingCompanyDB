using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using HatchlingCompany.Models.Common;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class CreateEmployee : Command
    {
        private readonly IHatchlingCompanyDbContext db;

        public CreateEmployee(IHatchlingCompanyDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public override void Execute()
        {
            var parameters = this.Parameters;
            var firstName = parameters[1];
            var lastName = parameters[2];
            var email = parameters[3];
            var phoneNumber = parameters[4];
            var status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), parameters[5].ToLower());

            var employeeFound = this.db.Employees.SingleOrDefault(e => e.PhoneNumber == phoneNumber);

            if (employeeFound != null)
            {
                throw new ArgumentNullException($"{employeeFound.FirstName} {employeeFound.LastName} already exists");
            }

            this.db.Employees.Add(new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Status = status
            });

            this.db.SaveChanges();
        }
    }
}
