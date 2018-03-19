using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class CreateManager : Command
    {
        private readonly IDbContext db;

        public CreateManager(IDbContext db)
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

            var manager = this.db.Employees.SingleOrDefault(e => e.Email == email);

            if (manager != null)
            {
                throw new ArgumentNullException($"{manager.FirstName} {manager.LastName} already exists");
            }

            this.db.Employees.Add(new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            });

            this.db.SaveChanges();
        }
    }
}
