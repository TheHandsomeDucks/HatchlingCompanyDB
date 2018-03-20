using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data;
using HatchlingCompany.Models.Common;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class UpdateEmployeeStatus : Command
    {
        private readonly IDbContext db;

        public UpdateEmployeeStatus(IDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public override void Execute()
        {
            var parameters = this.Parameters;
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
        }
    }
}
