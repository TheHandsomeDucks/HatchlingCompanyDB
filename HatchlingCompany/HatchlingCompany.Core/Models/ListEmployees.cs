using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListEmployees : IMapFrom<Employee>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Full Name: {this.FirstName} {this.LastName}");
            sb.AppendLine($"Job Title: {this.JobTitle}");
            return sb.ToString();
        }
    }
}
