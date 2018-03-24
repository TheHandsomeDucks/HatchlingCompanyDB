using AutoMapper;
using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.CRUD
{
    [TestClass]
    public class FindEmployeeByMailTests
    {
        private FindEmployeeByMail findEmployeeByMailService;
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
            this.findEmployeeByMailService = new FindEmployeeByMail(dbStub, writerStub.Object);
        }

        [TestMethod]
        public void FindEmployeeByMail_Should_Throw_ArgumentException_If_No_Email_Is_Passed()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findEmployeeByMail"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => findEmployeeByMailService.Execute(parameters));
        }

        [TestMethod]
        public void FindEmployeeByMail_Should_Throw_ArgumentException_If_No_Email_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findEmployeeByMail", null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => findEmployeeByMailService.Execute(parameters));
        }

        [TestMethod]
        public void FindEmployeeByMail_Should_Throw_ArgumentException_If_No_Email_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findEmployeeByMail", ""
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => findEmployeeByMailService.Execute(parameters));
        }

        [TestMethod]
        public void FindEmployeeByMail_Should_Throw_ArgumentException_If_No_Email_Is_WhiteSpace()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findEmployeeByMail",
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => findEmployeeByMailService.Execute(parameters));
        }

        [TestMethod]
        public void FindEmployeeByMail_Should_Throw_ArgumentNullException_If_Employee_With_Passed_Email_Does_Not_Exists()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findEmployeeByMail", "noneExistingMail"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => findEmployeeByMailService.Execute(parameters));
        }

        [TestMethod]
        public void FindEmployeeByMail_Should_Return_Employee_TypeOf_ListEmployeeDetailsModel_If_Employee_Exists()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = "alex@gmail.com"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com"
            });

            // Act
            findEmployeeByMailService.Execute(new List<string>()
            {
                "createEmployee", "alex@gmail.com"
            });

            var employeeExists = this.dbStub
                             .Employees
                             .Where(e => e.Email == "alex@gmail.com")
                             .ProjectTo<ListEmployeeDetailsModel>()
                             .SingleOrDefault();

            // Assert
            Assert.IsInstanceOfType(employeeExists, typeof(ListEmployeeDetailsModel));
        }

        [TestMethod]
        public void FindEmployeeByMail_Should_Return_Concrete_Employee_If_Existing_Email_Is_Passed()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = "alex@gmail.com"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com"
            });

            // Act
            findEmployeeByMailService.Execute(new List<string>()
            {
                "createEmployee", "alex@gmail.com"
            });

            var employeeExists = dbStub.Employees.SingleOrDefault(e => e.Email == "alex@gmail.com");

            // Assert
            Assert.AreEqual("alex@gmail.com", employeeExists.Email);
        }

        [TestMethod]
        public void FindEmployeeByMail_Should_Return_Employee_TypeOf_ListEmployeeDetailsModel__With_Correcttly_Mapped_Props_If_Employee_Exists()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = "alex@gmail.com"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com"
            });

            // Act
            findEmployeeByMailService.Execute(new List<string>()
            {
                "createEmployee", "alex@gmail.com"
            });

            var employeeExists = this.dbStub
                             .Employees
                             .Where(e => e.Email == "alex@gmail.com")
                             .ProjectTo<ListEmployeeDetailsModel>()
                             .SingleOrDefault();

            // Assert
            Assert.AreEqual("Alex", employeeExists.FirstName);
            Assert.AreEqual("Alexov", employeeExists.LastName);
            Assert.AreEqual("alex@gmail.com", employeeExists.Email);
        }
    }
}
