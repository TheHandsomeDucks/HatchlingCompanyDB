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
    public class ListEmployeeDetailsTests
    {
        private CreateEmployee createEmployeeService;
        private ListEmployeeDetails listEmployeeDetailsService;
        private Mock<IMapper> mapperStub;
        private Mock<IWriter> writerStub;
        private IDbContext dbStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.writerStub = new Mock<IWriter>();
            this.mapperStub = new Mock<IMapper>();
            this.listEmployeeDetailsService = new ListEmployeeDetails(dbStub, writerStub.Object);
            this.createEmployeeService = new CreateEmployee(dbStub, writerStub.Object, mapperStub.Object);
        }

        [TestMethod]
        public void ListEmployeeDetails_Should_Call_Concret_Employee()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Ace",
                LastName = "Base",
                Email = "ace@gmail.com"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Ace", "Base", "ace@gmail.com"
            });

            var listEmployeeDetailsService = new ListEmployeeDetails(dbStub, writerStub.Object);

            // Act
            listEmployeeDetailsService.Execute(new List<string>()
            {
                "listemployeedetails", "ace@gmail.com"
            });

            var employeeExists = dbStub.Employees.SingleOrDefault(e => e.Email == "ace@gmail.com");

            // Assert
            Assert.AreEqual("Ace", employeeExists.FirstName);
            Assert.AreEqual("Base", employeeExists.LastName);
            Assert.AreEqual("ace@gmail.com", employeeExists.Email);
        }

        [TestMethod]
        public void ListEmployeeDetails_Should_Call_Mapper_Once()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Ace",
                LastName = "Base",
                Email = "ace@gmail.com"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Ace", "Base", "ace@gmail.com"
            });

            var listEmployeeDetailsService = new ListEmployeeDetails(dbStub, writerStub.Object);

            // Act
            listEmployeeDetailsService.Execute(new List<string>()
            {
                "listemployeedetails", "ace@gmail.com"
            });

            // Assert
            mapperStub.Verify(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>()), Times.Once);
        }
    }
}
