﻿using Autofac;
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

            // --------------------- CRUD -------------------------

            // Manager
            builder.RegisterType<CreateManager>().Named<ICommand>("createmanager").InstancePerDependency();

            // Employees
            builder.RegisterType<CreateEmployee>().Named<ICommand>("createemployee").InstancePerDependency();
            builder.RegisterType<FindEmployeeByMail>().Named<ICommand>("findemployeebymail").InstancePerDependency();

            // Projects
            builder.RegisterType<CreateProject>().Named<ICommand>("createproject").InstancePerDependency();
            builder.RegisterType<FindProjectByName>().Named<ICommand>("findprojectbyname").InstancePerDependency();

            // ------------------   Listing  ---------------------------------

            // Help
            builder.RegisterType<Help>().Named<ICommand>("help").InstancePerDependency();

            // Employees
            builder.RegisterType<ListEmployees>().Named<ICommand>("listemployees").InstancePerDependency();
            builder.RegisterType<ListEmployeeDetails>().Named<ICommand>("listemployeeDetails").InstancePerDependency();
            builder.RegisterType<UpdateEmployeeStatus>().Named<ICommand>("updateemployeeStatus").InstancePerDependency();
            builder.RegisterType<ListEmployeesByStatus>().Named<ICommand>("listemployeesByStatus").InstancePerDependency();

            // Projects
            builder.RegisterType<ListProjects>().Named<ICommand>("listprojects").InstancePerDependency();
            builder.RegisterType<ListProjectDetails>().Named<ICommand>("listprojectdetails").InstancePerDependency();
        }
    }
}
