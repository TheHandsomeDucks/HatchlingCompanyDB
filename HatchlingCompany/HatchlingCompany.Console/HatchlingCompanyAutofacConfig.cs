using Autofac;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using System.Data.Entity;
using System.Reflection;

namespace HatchlingCompany.Console
{
    public class HatchlingCompanyAutofacConfig : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Data
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(DbContext)))
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

            // DbContext
            builder.RegisterType<HatchlingCompanyDbContext>().As<IDbContext>().InstancePerDependency();

            // CRUD Named Services
            builder.RegisterType<CreateEmployeeCommand>().Named<ICommand>("createemployee").InstancePerDependency();
            builder.RegisterType<FindEmployeeByMailCommand>().Named<ICommand>("findemployeebymail").InstancePerDependency();

            // Listing Named Services
            builder.RegisterType<HelpCommand>().Named<ICommand>("help").InstancePerDependency();
            builder.RegisterType<ListEmployeesCommand>().Named<ICommand>("listemployees").InstancePerDependency();
            builder.RegisterType<ListEmployeeDetailsCommand>().Named<ICommand>("listemployeedetails").InstancePerDependency();
        }
    }
}
