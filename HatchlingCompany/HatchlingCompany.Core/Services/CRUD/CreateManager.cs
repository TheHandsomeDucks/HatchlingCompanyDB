﻿using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HatchlingCompany.Core.Services.CRUD
{
    public class CreateManager : ICommand
    {
        private readonly IDbContext db;

        public CreateManager(IDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            //var parameters = this.Parameters;
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

            return $"A new manager with name {firstName} {lastName} was created.";
        }
    }
}
