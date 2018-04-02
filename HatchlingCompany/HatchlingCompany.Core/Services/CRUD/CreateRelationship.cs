using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using HatchlingCompany.Models;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class CreateRelationship : ICommand
    {
        private IDbContext db;
        private IWriter writer;
        private IMapper mapper;

        public CreateRelationship(IDbContext db, IWriter writer, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count < 4)
            {
                throw new ArgumentException(
                    "Invalid parameters! Please type in CreateRelationship [First_Employee_Email] [Second_Employee_Email] [Relationship_Strength] [Comment *optional*]!");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentException("First Employee Email cannot be null, empty or whitespace!");
            }

            if (!Regex.IsMatch(parameters[1], @"[0-9,A-z]*@[A-z,0-9]*.[A-z]*"))
            {
                throw new ArgumentException("Invalid first email address.");
            }

            if (String.IsNullOrEmpty(parameters[2]) || String.IsNullOrWhiteSpace(parameters[2]))
            {
                throw new ArgumentException("Second Employee Email cannot be null, empty or whitespace!");
            }

            if (!Regex.IsMatch(parameters[2], @"[0-9,A-z]*@[A-z,0-9]*.[A-z]*"))
            {
                throw new ArgumentException("Invalid second email address.");
            }

            if (!int.TryParse(parameters[3], out var relationshipStrength) || relationshipStrength < 0 || 9 < relationshipStrength )
            {
                throw new ArgumentException("Relationship Strength should be integer between 0 and 9");
            }

            var firstEmployeeEmail = parameters[1];
            var secondEmployeeEmail = parameters[2];

            if (firstEmployeeEmail.Equals(secondEmployeeEmail))
            {
                throw new ArgumentException("Employee emails cannot be equal");
            }

            var firstEmployee = this.db
                .Employees
                .Where(e => e.Email == firstEmployeeEmail)
                .SingleOrDefault();

            if (firstEmployee == null)
            {
                throw new ArgumentException($"Employee with Email \"{firstEmployeeEmail}\" could not be found!");
            }

            var secondEmployee = this.db
                .Employees
                .Where(e => e.Email == secondEmployeeEmail)
                .SingleOrDefault();

            if (secondEmployee == null)
            {
                throw new ArgumentException($"Employee with Email \"{secondEmployeeEmail}\" could not be found!");
            }

            if (firstEmployee.Id > secondEmployee.Id)
            {
                var tempEmployee = firstEmployee;
                firstEmployee = secondEmployee;
                secondEmployee = tempEmployee;
            }

            if (this.db
                    .Relationships
                    .SingleOrDefault(e => (e.FirstEmployeeId == firstEmployee.Id || e.SecondEmployeeId == firstEmployee.Id)
                                       && (e.FirstEmployeeId == secondEmployee.Id || e.SecondEmployeeId == secondEmployee.Id)) != null)
            {
                throw new ArgumentException($"Relationship already exists");
            }

            var comment = String.Empty;

            if (parameters.Count > 4)
            {
                comment = string.Join(" ", parameters.Skip(4));
            }

            var relationship = new CreateRelationshipModel()
            {
                FirstEmployeeId = firstEmployee.Id,
                SecondEmployeeId = secondEmployee.Id,
                RelationshipStrength = relationshipStrength,
                Comment = comment
            };

            var toAdd = this.mapper.Map<Relationship>(relationship);

            this.db.Relationships.Add(toAdd);
            this.db.SaveChanges();

            var output = $"Successfully added relationship to {firstEmployeeEmail} and {secondEmployeeEmail}: {relationshipStrength}";
            this.writer.WriteLine(output);
        }
    }
}
