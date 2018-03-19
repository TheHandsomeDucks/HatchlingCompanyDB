using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListProjectModel : IMapFrom<Project>
    {
        public string Name { get; set; }

        public Employee Manager { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Project Name: {this.Name}");
            sb.AppendLine($"Manager: {this.Manager.FirstName} {this.Manager.LastName}");
            sb.AppendLine($"Manager Id: {this.Manager.Id}");
            return sb.ToString();
        }
    }
}
