using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListEmployeesModel : IMapFrom<Employee>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Full Name: {this.FirstName} {this.LastName}");
            sb.AppendLine($"Email: {this.Email}");
            return sb.ToString();
        }
    }
}
