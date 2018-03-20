using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Emplyoee
{
    [TestClass]
    public class CreateEmployeeTests
    {
        [TestMethod]
        public void CreateEmployee_Should_Create_Employee()
        {
            // Arragne
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var createEmployeeService = new CreateEmployee(dbMock);
            var createManagerService = new CreateManager(dbMock);

            createManagerService.Execute(new List<string>()
            {
                "createManager", "Manager", "Manager", "mailm", "phone"
            });

            //Act
            var returnMessage = createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "maila", "phone", "1"
            });

            var employee = dbMock.Employees.SingleOrDefault(e => e.Email == "maila");

            // Assert
            Assert.AreEqual(2, dbMock.Employees.Count());
            Assert.AreEqual("maila", employee.Email);
            Assert.AreEqual(returnMessage, "A new employee with name Alex Alexov was created.");
        }
    }
}
