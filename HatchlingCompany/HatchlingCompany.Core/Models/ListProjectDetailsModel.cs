using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using System.Collections.Generic;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListProjectDetailsModel : IMapFrom<Project>
    {
        public string Name { get; set; }

        public int ManagerId { get; set; }

        public Employee Manager { get; set; }

        public string Detail { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Project Name: {this.Name}");
            sb.AppendLine($"Manager: {this.Manager.FirstName} {this.Manager.LastName}");
            sb.AppendLine($"Manager Id: {this.Manager.Id}");
            sb.AppendLine($"Details: {this.Detail}");
            return sb.ToString();
        }
    }
}
