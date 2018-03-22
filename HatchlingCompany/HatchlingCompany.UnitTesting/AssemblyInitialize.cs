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
            AutomapperConfig.Initialize();
        }
    }
}
