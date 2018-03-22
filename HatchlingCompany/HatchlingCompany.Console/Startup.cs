using Autofac;
using HatchlingCompany.Core;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Data.Migrations;
using System.Data.Entity;
using System.Reflection;

namespace HatchlingCompany.Data
{
    public class Startup
    {
        public static void Main()
        {
            var strategy = new MigrateDatabaseToLatestVersion<HatchlingCompanyDbContext, Configuration>();
            Database.SetInitializer(strategy);

            var config = new AutomapperConfig();
            config.Initialize();

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var engine = container.Resolve<IEngine>();

            engine.Start();
        }
    }
}
