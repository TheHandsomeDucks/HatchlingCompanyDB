using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using System;
using System.Linq;
using System.Text;

namespace HatchlingCompany.Core.Services.Listing
{
    public class ListProjectDetails : Command
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListProjectDetails(IDbContext db, IWriter writer)
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

            var sb = new StringBuilder();
            sb.AppendLine("Listing project details...");

            var projectDetails = this.db
                                    .Projects
                                    .ProjectTo<ListProjectDetailsModel>()
                                    .SingleOrDefault();

            sb.AppendLine(projectDetails.PrintInfo());

            this.writer.WriteLine(sb.ToString());
        }
    }
}
