using HatchlingCompany.Core.Common.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatchlingCompany.UnitTesting.Services.Employees
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
