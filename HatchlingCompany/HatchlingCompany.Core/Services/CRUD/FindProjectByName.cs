using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Services.CRUD
{
    public class FindProjectByName : Command
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public FindProjectByName(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public override void Execute()
        {
            var parameters = this.Parameters;
            var name = parameters[1];

            var project = this.db.Projects.SingleOrDefault(p => p.Name == name);

            if (project == null)
            {
                throw new ArgumentNullException($"Project with {name} could not be found");
            }

            this.writer.WriteLine($"Name: {project.Name}");
            this.writer.WriteLine($"Manager: {project.Manager.FirstName} {project.Manager.LastName}");
            this.writer.WriteLine($"Details: {project.Detail}");
        }
    }
}
