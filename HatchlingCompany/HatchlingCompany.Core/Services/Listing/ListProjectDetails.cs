using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Core.Services.Listing
{
    public class ListProjectDetails : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListProjectDetails(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count != 2)
            {
                throw new ArgumentNullException("Parameters are invalid");
            }

            if (String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace");
            }

            if (String.IsNullOrEmpty(parameters[1]) || String.IsNullOrWhiteSpace(parameters[1]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace");
            }

            var name = parameters[1];

            var project = this.db
                        .Projects
                        .Where(e => e.Name == name)
                        .ProjectTo<ListProjectDetailsModel>()
                        .SingleOrDefault();

            if (project == null)
            {
                throw new ArgumentNullException($"Project with {name} could not be found");
            }

            var sb = new StringBuilder();
            sb.AppendLine("Listing project details...");
            sb.AppendLine(project.PrintInfo());
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine($"All details for project {project.Name} have been listed");
        }
    }
}
