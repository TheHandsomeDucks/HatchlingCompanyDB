using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
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
        private readonly IMapper mapper;

        public CreateEmployee(IDbContext db, IWriter writer, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var employee = new CreateEmployeeModel
            {
                FirstName = parameters[1],
                LastName = parameters[2],
                Email = parameters[3]
            };

            var employeeExists = this.db.Employees.SingleOrDefault(e => e.Email == employee.Email);

            if (employeeExists != null)
            {
                throw new ArgumentNullException($"{employeeExists.FirstName} {employeeExists.LastName} already exists");
            }

            var employeeToAdd = this.mapper.Map<Employee>(employee);

            this.db.Employees.Add(employeeToAdd);

            this.db.SaveChanges();

            this.writer.WriteLine($"A new employee with name {employeeToAdd.FirstName} {employeeToAdd.LastName} was created.");
        }
    }
}
