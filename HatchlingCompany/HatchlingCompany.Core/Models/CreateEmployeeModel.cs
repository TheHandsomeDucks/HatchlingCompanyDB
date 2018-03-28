using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Models;

namespace HatchlingCompany.Core.Models
{
    public class CreateEmployeeModel : IMapFrom<Employee>, ICustomMapping
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Employee, CreateEmployeeModel>();
        }
    }
}
