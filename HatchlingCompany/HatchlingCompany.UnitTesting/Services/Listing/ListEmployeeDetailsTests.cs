using AutoMapper;
using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
        public void ListEmployeeDetails_Should_Throw_ArgumentNullException_If_FirstParameter_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                null, "alex@gmail.com"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeeDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployeeDetails_Should_Throw_ArgumentNullException_If_FirstParameter_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "", "alex@gmail.com"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeeDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployeeDetails_Should_Throw_ArgumentNullException_If_SecondtParameter_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "listEmployeeDetails", null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeeDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployeeDetails_Should_Throw_ArgumentNullException_If_SecondParameter_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "listEmployeeDetails", ""
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeeDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployeeDetails_Should_Throw_ArgumentNullException_If_Employee_Is_Null()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeeDetailsService.Execute(new List<string>()
            {
                "listEmployeeDetails", "noneExistingMail"
            }));
        }

        [TestMethod]
        public void ListEmployeeDetails_Should_Find_Concrete_Employee()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = "alex@gmail.com",
                PhoneNumber = "123456789"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com", "123456789"
            });

            // Act
            listEmployeeDetailsService.Execute(new List<string>()
            {
                "listemployeedetails", "alex@gmail.com"
            });

            var employee = this.dbStub
                             .Employees
                             .Where(e => e.Email == "alex@gmail.com")
                             .ProjectTo<ListEmployeeDetailsModel>()
                             .SingleOrDefault();

            // Assert
            Assert.AreEqual("Alex", employee.FirstName);
            Assert.AreEqual("Alexov", employee.LastName);
            Assert.AreEqual("alex@gmail.com", employee.Email);
        }
    }
}
