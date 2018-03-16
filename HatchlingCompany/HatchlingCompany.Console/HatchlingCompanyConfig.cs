using Autofac;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Contracts;
using System.Reflection;

namespace HatchlingCompany.Console
{
    public class HatchlingCompanyConfig : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            // reg Core
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

        }
    }
}
