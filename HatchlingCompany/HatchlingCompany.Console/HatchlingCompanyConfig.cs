using Autofac;

namespace HatchlingCompany.Console
{
    public class HatchlingCompanyConfig : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContainerBuilder>().AsSelf().SingleInstance();
        }
    }
}
