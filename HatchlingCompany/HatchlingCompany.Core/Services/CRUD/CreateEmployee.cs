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
            if (parameters == null || parameters.Count != 5)
            {
                throw new ArgumentException("Invalid parameters! Please type in CreateEmployee [fistName] [lastName] [email] [phoneNumber]!");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentException("First Name cannot be null, empty or whitespace!");
            }

            if (String.IsNullOrEmpty(parameters[2]) || String.IsNullOrWhiteSpace(parameters[2]))
            {
                throw new ArgumentException("Last Name cannot be null, empty or whitespace!");
            }

            if (String.IsNullOrEmpty(parameters[3]) || String.IsNullOrWhiteSpace(parameters[3]))
            {
                throw new ArgumentException("Email name cannot be null, empty or whitespace!");
            }

            if (String.IsNullOrEmpty(parameters[4]) || String.IsNullOrWhiteSpace(parameters[4]))
            {
                throw new ArgumentException("Phone Number name cannot be null, empty or whitespace!");
            }

            var employee = new CreateEmployeeModel
            {
                FirstName = parameters[1],
                LastName = parameters[2],
                Email = parameters[3],
                PhoneNumber = parameters[4]
            };

            var employeeExists = this.db.Employees.SingleOrDefault(e => e.Email == employee.Email);

            if (employeeExists != null)
            {
                throw new ArgumentException($"Employee {employeeExists.FirstName} {employeeExists.LastName} already exists!");
            }

            var employeeToAdd = this.mapper.Map<Employee>(employee);

            this.db.Employees.Add(employeeToAdd);

            this.db.SaveChanges();

            this.writer.WriteLine($"A new employee {employeeToAdd.FirstName} {employeeToAdd.LastName} was created.");
        }
    }
}
