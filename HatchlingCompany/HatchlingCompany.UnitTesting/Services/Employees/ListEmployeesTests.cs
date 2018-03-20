using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Employees
{
    [TestClass]
    public class ListEmployeesTests
    {
        [TestMethod]
        public void ListEmployees_Should_Call_PrintInfo_Of_All_Employee()
        {
            // Arrange 
            // create employees
            AutoMapperProfile.Initialize();
            var writerMock = new Mock<IWriter>();
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var createEmployeeService = new CreateEmployee(dbMock, writerMock.Object);

            var employeeAlex = new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com", "phone"
            };

            var employeeJohn = new List<string>()
            {
                "createEmployee", "John", "Dow", "john@gmail.com", "phone"
            };
            createEmployeeService.Execute(employeeAlex);
            createEmployeeService.Execute(employeeJohn);

            // create listEmployees
            var listEmployeesService = new ListEmployees(dbMock, writerMock.Object);
            var parameters = new List<string>()
            {
                "listemployees"
            };

            // Act
            listEmployeesService.Execute(parameters);

            var employees = dbMock.Employees.ToList();

            // Assert
            Assert.IsNotNull(employees);
            Assert.AreEqual(employees[0].FirstName, "Alex");
            Assert.AreEqual(employees[1].FirstName, "John");
        }
    }
}
