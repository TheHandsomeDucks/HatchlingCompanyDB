using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
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
        private readonly IWriter writer;
        private readonly IMapper mapper;

        public CreateProject(IDbContext db, IWriter writer, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var project = new CreateProjectModel
            {
                Name = parameters[1]
            };


            var projectExists = this.db.Projects.SingleOrDefault(p => p.Name == project.Name);

            if (projectExists != null)
            {
                throw new ArgumentNullException($"{projectExists.Name} already exists");
            }

            var projectToAdd = this.mapper.Map<Project>(project);

            this.db.Projects.Add(projectToAdd);

            this.db.SaveChanges();

            this.writer.WriteLine($"A new project with name {projectToAdd.Name} was created.");
        }
    }
}
