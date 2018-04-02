using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Data;
using HatchlingCompany.Models;

namespace HatchlingCompany.Core.Services.Listing
{
    public class ListRelationshipsByEmail : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;
        private readonly IMapper mapper;

        public ListRelationshipsByEmail(IDbContext db, IWriter writer, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count != 2)
            {
                throw new ArgumentNullException(
                    "Invalid parameters! Please type in ListRelationshipsByEmail [Employee_Email]");
            }
            
            if (String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentException("Command cannot be null, empty or whitespace!");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentNullException("Status cannot be null, empty or whitespace!");
            }

            if (!Regex.IsMatch(parameters[1], @"[0-9,A-z]*@[A-z,0-9]*.[A-z]*"))
            {
                throw new ArgumentException("Invalid email address.");
            }

            var email = parameters[1];
            var employee = this.db.Employees
                                    .Where(e => e.Email == email)
                                    .SingleOrDefault();

            if (employee == null)
            {
                throw new ArgumentException("No such employee.");
            }

            var relationships = this.db.Relationships
                    .Where(x => (x.FirstEmployee.Id.Equals(employee.Id))
                             || (x.SecondEmployee.Id.Equals(employee.Id)))
                .ToList();

            if (!relationships.Any())
            {
                throw new ArgumentException($"Employee {email} has no relationships");
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Listing relationships for employee {email}");
            relationships.ForEach(e => sb.AppendLine($"{e.FirstEmployee.Email} {e.SecondEmployee.Email}: {e.RelationshipStrength} {(e.Comment.Length > 20 ? e.Comment.Substring(0, 20) : e.Comment)}"));
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine($"All relationships for employee {email} have been listed");
        }
    }
}