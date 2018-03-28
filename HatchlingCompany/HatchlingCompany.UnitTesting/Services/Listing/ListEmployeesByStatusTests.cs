using AutoMapper;
using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using HatchlingCompany.Models.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Listing
{
    [TestClass]
    public class ListEmployeesByStatusTests
    {
        private CreateEmployee createEmployeeService;
        private ListEmployeesByStatus listEmployeesByStatusService;
        private Mock<IMapper> mapperStub;
        private Mock<IWriter> writerStub;
        private IDbContext dbStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.writerStub = new Mock<IWriter>();
            this.mapperStub = new Mock<IMapper>();
            this.listEmployeesByStatusService = new ListEmployeesByStatus(dbStub, writerStub.Object);
            this.createEmployeeService = new CreateEmployee(dbStub, writerStub.Object, mapperStub.Object);
        }

        [TestMethod]
        public void ListEmployeesByStatus_Should_Throw_ArgumentNullException_If_FirstParameter_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                null, "AnyStatus"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeesByStatusService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployeesByStatus_Should_Throw_ArgumentNullException_If_FirstParameter_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "", "AnyStatus"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeesByStatusService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployeesByStatus_Should_Throw_ArgumentNullException_If_SecondtParameter_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "listEmployeesByStatus", null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeesByStatusService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployeesByStatus_Should_Throw_ArgumentNullException_If_SecondParameter_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "listEmployeesByStatus", ""
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeesByStatusService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployeesByStatus_Should_Throw_ArgumentNullException_If_Status_Could_Not_Be_Parsed()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentException>(() => listEmployeesByStatusService.Execute(new List<string>()
            {
                "listEmployeesByStatus", "nonExistingStatus"
            }));
        }

        [TestMethod]
        public void ListEmployeesByStatus_Should_Throw_ArgumentNullException_If_Employees_With_That_Status_Could_Not_Be_Found()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentException>(() => listEmployeesByStatusService.Execute(new List<string>()
            {
                "listEmployeesByStatus", "1"
            }));
        }

        [TestMethod]
        public void ListEmployeesByStatus_Should_Return_All_Employees_With_That_Status()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = "alex@gmail.com"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            // Act
            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com"
            });

            var employee = this.dbStub.Employees.SingleOrDefault(e => e.Email == "alex@gmail.com");

            var status = EmployeeStatus.Assigned;
            employee.Status = status;
            dbStub.SaveChanges();


            // Act
            listEmployeesByStatusService.Execute(new List<string>()
            {
                 "listEmployeesByStatus", "1"
            });

            var employees = this.dbStub
                          .Employees
                          .Where(e => e.Status == status)
                          .ProjectTo<ListEmployeesByStatusModel>()
                          .ToList();

            // Assert
            Assert.IsNotNull(employees);
            Assert.AreEqual(status, employees[0].Status);
        }
    }
}
