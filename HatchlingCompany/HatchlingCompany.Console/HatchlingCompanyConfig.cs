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
            // register Data layer
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IHatchlingCompanyDbContext)))
               .AsImplementedInterfaces()
               .SingleInstance();

            // register Core layer
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IEngine)))
                .Where(x => x.Namespace.Contains("Common") ||
                            x.Namespace.Contains("Factories") ||
                            x.Namespace.Contains("Models") ||
                            x.Name.EndsWith("Engine"))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<AutoMapperProfile>().AsSelf().SingleInstance();
            builder.Register(x => Mapper.Instance);

            // reg Autofac
            builder.RegisterType<ContainerBuilder>().AsSelf().SingleInstance();

            // CRUD Commands
            builder.RegisterType<CreateEmployeeCommand>().Named<ICommand>("createEmployee").SingleInstance();

            builder.RegisterType<FindEmployeeByMailCommand>().Named<ICommand>("findEmployeeByMail").SingleInstance();

            // Listing Commands
            builder.RegisterType<HelpCommand>().Named<ICommand>("help").SingleInstance();
            builder.RegisterType<ListAllEmployeesCommand>().Named<ICommand>("listAllEmployees").SingleInstance();
        }
    }
}
