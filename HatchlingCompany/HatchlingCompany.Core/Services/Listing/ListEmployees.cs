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
    public class ListEmployees : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListEmployees(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var employees = this.db
                            .Employees
                            .ProjectTo<ListEmployeesModel>()
                            .ToList();

            if (!employees.Any())
            {
                throw new ArgumentNullException("No employees registered");
            }

            var sb = new StringBuilder();
            sb.AppendLine("Listing employees...");

            foreach (var employee in employees)
            {
                sb.AppendLine(employee.PrintInfo());
            }

            this.writer.WriteLine(sb.ToString());

            return $"All employees have been listed";
        }
    }
}
