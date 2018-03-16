using HatchlingCompany.Core.Contracts;
using HatchlingCompany.Data;
using HatchlingCompany.Models.Common;
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
            var firstName = parameters[1];
            var lastName = parameters[2];
            var phoneNumber = parameters[3];
            var email = parameters[4];
            var role = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), parameters[5].ToLower());

            var employee = this.db.Employees.SingleOrDefault(e => e.PhoneNumber == phoneNumber);

            if (employee != null)
            {
                throw new ArgumentNullException($"{employee.FirstName} {employee.LastName} already exists");
            }

            this.db.Employees.Add(employee);
            this.db.SaveChanges();
        }
    }
}
