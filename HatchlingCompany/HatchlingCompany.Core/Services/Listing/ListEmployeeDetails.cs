﻿using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Core.Services.Listing
{
    public class ListEmployeeDetails : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListEmployeeDetails(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count != 2)
            {
                throw new ArgumentNullException("Invalid parameters! Please type in ListEmployeeDetals [Employee_Email]");
            }

            if (String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace");
            }

            var email = parameters[1];

            var employee = this.db
                             .Employees
                             .Where(e => e.Email == email)
                             .ProjectTo<ListEmployeeDetailsModel>()
                             .SingleOrDefault();

            if (employee == null)
            {
                throw new ArgumentNullException($"Person with {email} could not be found");
            }

            var sb = new StringBuilder();
            sb.AppendLine("Listing employee details...");
            sb.AppendLine(employee.PrintInfo());
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine($"All details for employee {employee.FirstName} {employee.LastName} have been listed");
        }
    }
}
