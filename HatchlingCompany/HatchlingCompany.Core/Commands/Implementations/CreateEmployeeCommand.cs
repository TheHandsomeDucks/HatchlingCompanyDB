using HatchlingCompany.Core.Contracts;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using HatchlingCompany.Models.Common;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Commands.Implementations
{
    public class CreateEmployeeCommand : Command
    {
        private readonly IDbContext db;

        public CreateEmployeeCommand(IDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public override void Execute()
        {
            #region Test
            //var parameters = this.Parameters;
            //var firstName = parameters[1];
            //var lastName = parameters[2];
            //var phoneNumber = parameters[3];
            //var email = parameters[4];
            var role = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), "Assigned");

            //var employee = this.db.Employees.SingleOrDefault(e => e.PhoneNumber == phoneNumber);

            //if (employee != null)
            //{
            //    throw new ArgumentNullException($"{employee.FirstName} {employee.LastName} already exists");
            //}
            #endregion

            var employee = new Employee() { Birthdate = DateTime.Now, HireDate = DateTime.Now, Status = role, Salary = 1000 };

            this.db.Employees.Add(employee);
            this.db.SaveChanges();
        }
    }
}
