using Autofac;
using HatchlingCompany.Core.Contracts;
using System.Reflection;

namespace HatchlingCompany.Data
{
    public class Startup
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var engine = container.Resolve<IEngine>();

            engine.Start();
        }
    }
}
