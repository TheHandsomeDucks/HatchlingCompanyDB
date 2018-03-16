using Autofac;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Contracts;
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
                .Where(x => x.Namespace.Contains("Commands") ||
                            x.Namespace.Contains("Common") ||
                            x.Namespace.Contains("Factories") ||
                            x.Namespace.Contains("Providers") ||
                            x.Name.EndsWith("Engine"))
                .AsImplementedInterfaces()
                .SingleInstance();

            // reg Autofac
            builder.RegisterType<ContainerBuilder>().AsSelf().SingleInstance();

            // Employee Commands
            builder.RegisterType<CreateEmployeeCommand>().Named<ICommand>("createemployee").SingleInstance();

            builder.RegisterType<FindEmployeeCommand>().Named<ICommand>("findemployee").SingleInstance();
        }
    }
}
