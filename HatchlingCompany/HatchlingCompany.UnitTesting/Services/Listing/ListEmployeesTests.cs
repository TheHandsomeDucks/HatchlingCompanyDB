using AutoMapper;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Listing
{
    [TestClass]
    public class ListEmployeesTests
    {
        private CreateEmployee createEmployeeService;
        private Mock<IMapper> mapperStub;
        private Mock<IWriter> writerStub;
        private IDbContext dbStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.writerStub = new Mock<IWriter>();
            this.mapperStub = new Mock<IMapper>();
            this.createEmployeeService = new CreateEmployee(dbStub, writerStub.Object, mapperStub.Object);
        }

        [TestMethod]
        public void ListEmployees_Should_Call_PrintInfo_Of_All_Employee()
        {
            // Arrange
            var dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var writerStub = new Mock<IWriter>();
            var mapperStub = new Mock<IMapper>();

            var employeeToReturn = new Employee
            {
                FirstName = "Harry",
                LastName = "Poter",
                Email = "harry@gmail.com"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            var createEmployeeService = new CreateEmployee(dbStub, writerStub.Object, mapperStub.Object);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Harry", "Poter", "harry@gmail.com"
            });

            var listEmployeesService = new ListEmployees(dbStub, writerStub.Object);

            // Act
            listEmployeesService.Execute(new List<string>()
            {
                "listemployees"
            });

            var employees = dbStub.Employees.ToList();

            // Assert
            Assert.IsNotNull(employees);
            Assert.AreEqual("Harry", employees[0].FirstName);
            Assert.AreEqual("Poter", employees[0].LastName);
            Assert.AreEqual("harry@gmail.com", employees[0].Email);
        }
    }
}
