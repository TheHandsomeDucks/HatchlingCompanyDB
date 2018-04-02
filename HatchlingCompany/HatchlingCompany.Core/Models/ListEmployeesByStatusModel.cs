using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using HatchlingCompany.Models.Common;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListEmployeesByStatusModel : IMapFrom<Employee>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public EmployeeStatus Status { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Name: {this.FirstName} {this.LastName}");
            sb.AppendLine($"Email: {this.Email}");
            sb.AppendLine($"Status: {this.Status}");
            return sb.ToString();
        }
    }
}
