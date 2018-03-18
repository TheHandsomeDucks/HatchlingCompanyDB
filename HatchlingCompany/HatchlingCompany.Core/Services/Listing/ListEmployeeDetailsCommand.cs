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
    public class ListEmployeeDetailsCommand : Command
    {
        private readonly IDbContext db;
        private readonly IWriter writer;

        public ListEmployeeDetails(IDbContext db, IWriter writer)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public override void Execute()
        {
            var parameters = this.Parameters;
            var email = parameters[1];

            var employee = this.db.Employees.SingleOrDefault(e => e.Email == email);

            if (employee == null)
            {
                this.writer.WriteLine($"Person with {email} could not be found");
            }


            var sb = new StringBuilder();
            sb.AppendLine("Listing employees details...");

            var employeeDetails = this.db
                                     .Employees
                                     .Where(e => e.Email == email)
                                     .ProjectTo<ListEmployeeDetailsModel>()
                                     .SingleOrDefault();


            //foreach (var prop in employeeDetails)
            //{
            //    if (prop != null)
            //    {
            //        sb.AppendLine(prop);
            //    }
            //}

            sb.AppendLine(employeeDetails.PrintInfo());

            this.writer.WriteLine(sb.ToString());

        }
    }
}
