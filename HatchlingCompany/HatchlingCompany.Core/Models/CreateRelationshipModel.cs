using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;

namespace HatchlingCompany.Core.Models
{
    public class CreateRelationshipModel : IMapFrom<Relationship>, ICustomMapping
    {
        public int FirstEmployeeId{ get; set; }
        
        public int SecondEmployeeId { get; set; }
        
        public string Comment { get; set; }

        public int RelationshipStrength { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Relationship, CreateRelationshipModel>()
                .ForMember(f => f.FirstEmployeeId, opt => opt.MapFrom(src => src.FirstEmployee.Id))
                .ForMember(s => s.SecondEmployeeId, opt => opt.MapFrom(src => src.SecondEmployee.Id));
        }
    }
}