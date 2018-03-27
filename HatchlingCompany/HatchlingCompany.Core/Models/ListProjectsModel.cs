using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListProjectsModel : IMapFrom<Project>
    {
        public string Name { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Project: {this.Name}");
            return sb.ToString();
        }
    }
}
