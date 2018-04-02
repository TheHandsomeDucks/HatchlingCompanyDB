using Autofac;
using AutoMapper;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Services;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Core.Services.Exporting;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using HatchlingCompany.Utils;
using HatchlingCompany.Utils.Contracts;
using System.Collections.Generic;
using System.Reflection;

namespace HatchlingCompany.Console.AutofacModules
{
    public class AutofacModule : Autofac.Module
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

            // DbContext
            builder.RegisterType<HatchlingCompanyDbContext>().As<IDbContext>().InstancePerDependency();

            // Automapper
            builder.Register(x => Mapper.Instance);

            // --------------------- CRUD -------------------------
            // Employees
            builder.RegisterType<CreateEmployee>().Named<ICommand>("createemployee").InstancePerDependency();
            builder.RegisterType<FindEmployeeByMail>().Named<ICommand>("findemployeebymail").InstancePerDependency();

            // Projects
            builder.RegisterType<CreateProject>().Named<ICommand>("createproject").InstancePerDependency();
            builder.RegisterType<FindProjectByName>().Named<ICommand>("findprojectbyname").InstancePerDependency();

            // Relationships
            builder.RegisterType<CreateRelationship>().Named<ICommand>("createrelationship").InstancePerDependency();

            // ------------------   Listing  ---------------------------------
            // Help
            builder.RegisterType<Help>().Named<ICommand>("help").InstancePerDependency();

            // Employees
            builder.RegisterType<ListEmployees>().Named<ICommand>("listemployees").InstancePerDependency();
            builder.RegisterType<ListEmployeeDetails>().Named<ICommand>("listemployeedetails").InstancePerDependency();
            builder.RegisterType<UpdateEmployeeStatus>().Named<ICommand>("updateemployeestatus").InstancePerDependency();
            builder.RegisterType<ListEmployeesByStatus>().Named<ICommand>("listemployeesbystatus").InstancePerDependency();

            // Projects
            builder.RegisterType<ListProjects>().Named<ICommand>("listprojects").InstancePerDependency();
            builder.RegisterType<ListProjectDetails>().Named<ICommand>("listprojectdetails").InstancePerDependency();
            builder.RegisterType<AssignToProject>().Named<ICommand>("assigntoproject").InstancePerDependency();
            builder.RegisterType<RemoveFromProject>().Named<ICommand>("removefromproject").InstancePerDependency();

            // Misc
            builder.RegisterType<JSONSerializer>().As<ISerializer>().InstancePerDependency();
            builder.RegisterType<JSONDeserializer<IList<Employee>>>().As<IDeserializer<IList<Employee>>>().InstancePerDependency();
            builder.RegisterType<FileExporter>().As<IExporter>().InstancePerDependency();
            builder.RegisterType<FileImporter>().As<IImporter>().InstancePerDependency();

            // Exporers & Importers
            builder.RegisterType<JSONExportEmployees>().Named<ICommand>("jsonexportemployees");
            builder.RegisterType<JSONImportEmployees>().Named<ICommand>("jsonimportemployees");
        }
    }
}
