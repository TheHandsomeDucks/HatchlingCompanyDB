using Autofac;
using HatchlingCompany.Console.Commands;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Common.Contracts;
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
                            x.Name.EndsWith("Engine"))
                .AsImplementedInterfaces()
                .SingleInstance();

            // reg Autofac
            builder.RegisterType<ContainerBuilder>().AsSelf().SingleInstance();

            // Employee Commands
            builder.RegisterType<CreateEmployeeCommand>().Named<ICommand>("createEmployee").SingleInstance();

            builder.RegisterType<FindEmployeeByMailCommand>().Named<ICommand>("findEmployee").SingleInstance();

            builder.RegisterType<HelpCommand>().Named<ICommand>("help").SingleInstance();
        }
    }
}
