using Autofac;
using AutoMapper;
using HatchlingCompany.Commands.Listing;
using HatchlingCompany.Console.Commands.CRUD;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data;
using System.Reflection;

namespace HatchlingCompany.Console
{
    public class HatchlingCompanyConfig : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Data
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IHatchlingCompanyDbContext)))
               .AsImplementedInterfaces()
               .InstancePerDependency();

            // Core
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IEngine)))
                .Where(x => x.Namespace.Contains("Common") ||
                            x.Namespace.Contains("Factories") ||
                            x.Namespace.Contains("Models") ||
                            x.Name.EndsWith("Engine"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // AutoMapper
            builder.RegisterType<AutoMapperProfile>().AsSelf();
            builder.Register(x => Mapper.Instance);

            // Autofac
            builder.RegisterType<ContainerBuilder>().AsSelf().InstancePerDependency();

            // CRUD Commands
            builder.RegisterType<CreateEmployeeCommand>().Named<ICommand>("createEmployee").InstancePerDependency();

            builder.RegisterType<FindEmployeeByMailCommand>().Named<ICommand>("findEmployeeByMail").InstancePerDependency();

            // Listing Commands
            builder.RegisterType<HelpCommand>().Named<ICommand>("help").InstancePerDependency();
            builder.RegisterType<ListEmployeesCommand>().Named<ICommand>("listAllEmployees").InstancePerDependency();
        }
    }
}
