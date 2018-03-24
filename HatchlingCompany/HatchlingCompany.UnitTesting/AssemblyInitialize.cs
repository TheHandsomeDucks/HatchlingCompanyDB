using HatchlingCompany.Core.Common.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HatchlingCompany.UnitTesting
{
    [TestClass]
    public static class AssemblyInitialize
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext test)
        {
            // Automapper
            var config = new AutomapperConfig();
            config.Initialize();

            //// Common
            //var dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            //var writerStub = new Mock<IWriter>();
            //var mapperStub = new Mock<IMapper>();

            //// Services
            //var createEmployeeService = new CreateEmployee(dbStub, writerStub.Object, mapperStub.Object);
        }
    }
}
