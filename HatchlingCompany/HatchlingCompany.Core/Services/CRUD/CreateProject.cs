using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class CreateProject : Command
    {
        private readonly IDbContext db;

        public CreateProject(IDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public override void Execute()
        {
            var parameters = this.Parameters;
            var name = parameters[1];
            var managerId = int.Parse(parameters[2]);

            var project = this.db.Projects.SingleOrDefault(p => p.Name == name);

            if (project != null)
            {
                throw new ArgumentNullException($"{project.Name} already exists");
            }

            this.db.Projects.Add(new Project
            {
                Name = name,
                ManagerId = managerId
            });

            this.db.SaveChanges();
        }
    }
}
