using AutoMapper;

namespace HatchlingCompany.Core.Common.Contracts
{
    public interface ICustomMapping
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
