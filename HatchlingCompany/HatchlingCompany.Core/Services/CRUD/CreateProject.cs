using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class CreateProject : ICommand
    {
        private readonly IDbContext db;

        public CreateProject(IDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var name = parameters[1];

            var project = this.db.Projects.SingleOrDefault(p => p.Name == name);

            if (project != null)
            {
                throw new ArgumentNullException($"{project.Name} already exists");
            }

            this.db.Projects.Add(new Project
            {
                Name = name
            });

            this.db.SaveChanges();

            return $"A new project with name {name} was created.";
        }
    }
}
