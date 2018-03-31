using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;

namespace HatchlingCompany.Core.Models
{
    public class CreateRelationshipModel : IMapFrom<Relationship>, ICustomMapping
    {
        public string FirstEmployeeEmail { get; set; }

        public string SecondEmployeeEmail { get; set; }

        public string Comment { get; set; }

        public int RelationshipStrength { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Relationship, CreateRelationshipModel>()
                .ForMember(f => f.FirstEmployeeEmail, opt => opt.MapFrom(src => src.FirstEmployee.Email))
                .ForMember(s => s.SecondEmployeeEmail, opt => opt.MapFrom(src => src.SecondEmployee.Email));
        }
    }
}