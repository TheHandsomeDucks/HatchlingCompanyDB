using Autofac;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Commands.Implementations;
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
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IDbContext)))
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

            // --------------------------- CRUD ---------------------------

            // Manager
            builder.RegisterType<CreateManager>().Named<ICommand>("createManager").InstancePerDependency();

            // Employees
            builder.RegisterType<CreateEmployee>().Named<ICommand>("createEmployee").InstancePerDependency();
            builder.RegisterType<FindEmployeeByMail>().Named<ICommand>("findEmployeeByMail").InstancePerDependency();

            // Projects
            builder.RegisterType<CreateProject>().Named<ICommand>("createProject").InstancePerDependency();
            builder.RegisterType<FindProjectByName>().Named<ICommand>("findProjectByName").InstancePerDependency();

            // ------------------------- Listing ----------------------------

            // Help
            builder.RegisterType<Help>().Named<ICommand>("help").InstancePerDependency();

            // Employees
            builder.RegisterType<ListEmployees>().Named<ICommand>("listEmployees").InstancePerDependency();
            builder.RegisterType<ListEmployeeDetails>().Named<ICommand>("listEmployeeDetails").InstancePerDependency();
            builder.RegisterType<UpdateEmployeeStatus>().Named<ICommand>("updateEmployeeStatus").InstancePerDependency();
            builder.RegisterType<ListEmployeesByStatus>().Named<ICommand>("listEmployeesByStatus").InstancePerDependency();

            // Projects
            builder.RegisterType<ListProjects>().Named<ICommand>("listProjects").InstancePerDependency();
            builder.RegisterType<ListProjectDetails>().Named<ICommand>("listProjectDetails").InstancePerDependency();
        }
    }
}
