using Autofac;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data.Migrations;
using System;
using System.Data.Entity;
using System.Reflection;
using System.Text;
using HatchlingCompany.Utils;

namespace HatchlingCompany.Data
{
    public class Startup
    {
        public static void Main()
        {
            var strategy = new MigrateDatabaseToLatestVersion<HatchlingCompanyDbContext, Configuration>();
            Database.SetInitializer(strategy);

            AutoMapperProfile.Initialize();

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var engine = container.Resolve<IEngine>();

            engine.Start();
        }
    }
}
