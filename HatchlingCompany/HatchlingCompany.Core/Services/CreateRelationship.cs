using System;
using System.Collections.Generic;
using System.Linq;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Data;

namespace HatchlingCompany.Core.Services
{
    public class CreateRelationship : ICommand
    {
        private IDbContext db;
        private IWriter writer;

        public CreateRelationship(IDbContext db, IWriter writer)
        {
            this.db = db;
            this.writer = writer;
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count < 3)
            {
                throw new ArgumentNullException(
                    "Invalid parameters! Please type in AssignToProject [Employee_Email] [Project_Name]!");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentException("Employee Email cannot be null, empty or whitespace!");
            }

            if (String.IsNullOrEmpty(parameters[2]) || String.IsNullOrWhiteSpace(parameters[2]))
            {
                throw new ArgumentException("Project Name cannot be null, empty or whitespace!");
            }

            var firstEmployeeEmail = parameters[1];
            var secondEmployeeEmail = parameters[2];

            if (firstEmployeeEmail.Equals(secondEmployeeEmail))
            {
                throw new InvalidOperationException("Employee emails cannot be equal");
            }

            if (firstEmployeeEmail.CompareTo(secondEmployeeEmail) > 0)
            {
                var tempEmployeeEmail = firstEmployeeEmail;
                firstEmployeeEmail = secondEmployeeEmail;
                secondEmployeeEmail = tempEmployeeEmail;
            }

            var firstEmployee = this.db
                .Employees
                .Where(e => e.Email == firstEmployeeEmail)
                .SingleOrDefault();

            if (firstEmployee == null)
            {
                throw new ArgumentNullException($"Employee with Email \"{firstEmployeeEmail}\" could not be found!");
            }

            var secondEmployee = this.db
                .Employees
                .Where(e => e.Email == secondEmployeeEmail)
                .SingleOrDefault();


            var relationshipStrength = int.Parse(parameters[3]);

            string comment;

            if (parameters.Count > 3)
            {
                comment = string.Join(" ", parameters.Skip(3));
            }

            
        }
    }
}
