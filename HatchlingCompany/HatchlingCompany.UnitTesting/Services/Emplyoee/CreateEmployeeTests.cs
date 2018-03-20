using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var employeeMock = new Mock<Employee>();

            var parameters = new List<string>()
            {
                "Pesho", "Velev", "mail", "phone", "1"
            };

            //Act
            createEmployeeService.Execute(parameters);

            dbMock.Employees.Add(employeeMock.Object);

            // Assert
            Assert.AreEqual(1, dbMock.Employees.Count());
        }
    }
}
