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
    public class ListProjects : ICommand
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListProjects(IDbContext db, IWriter writer)
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

            var projects = this.db
                               .Projects
                               .ProjectTo<ListProjectModel>()
                               .ToList();

            if (!projects.Any())
            {
                throw new ArgumentNullException("No projects registered");
            }

            var sb = new StringBuilder();
            sb.AppendLine("Listing projects...");

            foreach (var project in projects)
            {
                sb.AppendLine(project.PrintInfo());
            }

            this.writer.WriteLine(sb.ToString());

            return $"All project have been listed";
        }
    }
}
