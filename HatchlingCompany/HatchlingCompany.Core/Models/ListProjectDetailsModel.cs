using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;
using System.Text;

namespace HatchlingCompany.Core.Models
{
    public class ListProjectDetailsModel : IMapFrom<Project>, ICustomMapping
    {
        public string Name { get; set; }

        public string Detail { get; set; }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Project Name: {this.Name}");
            sb.AppendLine($"Details: {this.Detail}");
            return sb.ToString();
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Project, ListProjectDetailsModel>();
        }
    }
}
