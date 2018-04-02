using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.Core.Services
{
    public class RemoveFromProject : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public RemoveFromProject(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count != 3)
            {
                throw new ArgumentNullException("Invalid parameters! Please type in RemoveFromProject [Employee_Email] [Project_Name]!");
            }

            if (String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentException("Command cannot be null, empty or whitespace!");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentException("Employee Email cannot be null, empty or whitespace!");
            }

            if (String.IsNullOrEmpty(parameters[2]) || String.IsNullOrWhiteSpace(parameters[2]))
            {
                throw new ArgumentException("Project Name cannot be null, empty or whitespace!");
            }

            var employeeEmail = parameters[1];

            var employee = this.db
                             .Employees
                             .Where(e => e.Email == employeeEmail)
                             .SingleOrDefault();

            if (employee == null)
            {
                throw new ArgumentNullException($"Employee with Email \"{employeeEmail}\" could not be found!");
            }

            var projectName = parameters[2];

            var project = this.db
                            .Projects
                            .Where(p => p.Name == projectName)
                            .SingleOrDefault();

            if (project == null)
            {
                throw new ArgumentNullException($"Project {projectName} could not be found!");
            }

            if (!employee.Projects.Contains(project))
            {
                throw new ArgumentException($"Employee with email \"{employeeEmail}\" has not been assigned to Project {projectName}");
            }

            employee.Projects.Remove(project);
            project.Employees.Remove(employee);

            this.db.SaveChanges();

            this.writer.WriteLine($"Employee with email \"{employeeEmail}\" has been successfully removed from Project {projectName}");
        }
    }
}
