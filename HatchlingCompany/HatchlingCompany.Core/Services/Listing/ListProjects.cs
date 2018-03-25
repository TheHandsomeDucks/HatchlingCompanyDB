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

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count != 1 || String.IsNullOrEmpty(parameters[0]) || String.IsNullOrWhiteSpace(parameters[0]))
            {
                throw new ArgumentNullException("Command cannot be null, empty or whitespace");
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
            projects.ForEach(p => sb.AppendLine($"{p.PrintInfo()}"));
            this.writer.WriteLine(sb.ToString());
            this.writer.WriteLine($"All projects have been listed");
        }
    }
}
