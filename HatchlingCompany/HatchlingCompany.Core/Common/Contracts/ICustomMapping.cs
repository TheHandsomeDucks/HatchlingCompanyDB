using AutoMapper;

namespace HatchlingCompany.Core.Common.Contracts
{
    public interface ICustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
