using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;

namespace HatchlingCompany.Core.Models
{
    public class CreateProjectModel : IMapFrom<Project>, ICustomMapping
    {
        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Project, CreateProjectModel>();
        }
    }
}
