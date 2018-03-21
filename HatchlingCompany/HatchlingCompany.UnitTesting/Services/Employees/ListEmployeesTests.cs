using AutoMapper;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Employees
{
    [TestClass]
    public class ListEmployeesTests
    {
        //[ClassInitialize]
        //public static void InitilizeAutomapper(TestContext context)
        //{
        //    AutomapperConfig.Initialize();
        //}

        [TestMethod]
        public void ListEmployees_Should_Call_PrintInfo_Of_All_Employee()
        {
            // Arrange
            AutomapperConfig.Initialize();
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var writerMock = new Mock<IWriter>();
            var mapperMock = new Mock<IMapper>();

            var employeeToReturn = new Employee
            {
                FirstName = "Harry",
                LastName = "Poter",
                Email = "harry@gmail.com"
            };

            mapperMock.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            var createEmployeeService = new CreateEmployee(dbMock, writerMock.Object, mapperMock.Object);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Harry", "Poter", "harry@gmail.com"
            });

            //createEmployeeService.Execute(new List<string>()
            //{
            //    "createEmployee", "John", "Schmid", "schmid@gmail.com"
            //});


            var listEmployeesService = new ListEmployees(dbMock, writerMock.Object);

            // Act
            listEmployeesService.Execute(new List<string>()
            {
                "listemployees"
            });

            var employees = dbMock.Employees.ToList();

            // Assert
            Assert.IsNotNull(employees);
            Assert.AreEqual("Harry", employees[0].FirstName);
            Assert.AreEqual("Poter", employees[0].LastName);
            Assert.AreEqual("harry@gmail.com", employees[0].Email);

            //Assert.AreEqual("John", employees[1].FirstName);
            //Assert.AreEqual("Schmid", employees[1].LastName);
            //Assert.AreEqual("schmid@gmail.com", employees[1].Email);
        }
    }
}
