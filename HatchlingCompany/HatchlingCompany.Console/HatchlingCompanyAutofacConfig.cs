using Autofac;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using System.Reflection;

namespace HatchlingCompany.Console
{
    public class HatchlingCompanyAutofacConfig : Autofac.Module
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
                            x.Namespace.Contains("Services") ||
                            x.Name.EndsWith("Engine"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // CRUD Commands
            builder.RegisterType<CreateEmployee>().Named<ICommand>("createEmployee").InstancePerDependency();

            builder.RegisterType<FindEmployeeByMail>().Named<ICommand>("findEmployeeByMail").InstancePerDependency();

            // Listing Commands
            builder.RegisterType<Help>().Named<ICommand>("help").InstancePerDependency();
            builder.RegisterType<ListEmployee>().Named<ICommand>("listEmployees").InstancePerDependency();
        }
    }
}
