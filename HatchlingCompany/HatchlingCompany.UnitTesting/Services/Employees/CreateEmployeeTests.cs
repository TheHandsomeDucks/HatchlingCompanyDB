using AutoMapper;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Employees
{
    [TestClass]
    public class CreateEmployeeTests
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
        public void CreateEmployee_Should_Throw_ArgumentException_If_No_Params_Are_Passed()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "createEmployee"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => createEmployeeService.Execute(parameters));
        }

        [TestMethod]
        public void CreateEmployee_Should_Throw_ArgumentException_If_LastName_And_Email_Are_Not_Passed()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "createEmployee", "Alex"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => createEmployeeService.Execute(parameters));
        }

        [TestMethod]
        public void CreateEmployee_Should_Throw_ArgumentException_If_Email_Is_Not_Passed()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "createEmployee", "Alex", "Alexov"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => createEmployeeService.Execute(parameters));
        }

        [TestMethod]
        public void CreateEmployee_Should_Throw_DbEntityValidationException_If_Email_Is_Null()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = null
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            // Act
            var parameters = new List<string>()
            {
                "createEmployee", "Alex", "Alexov", null
            };

            // Assert
            Assert.ThrowsException<ArgumentException>(() => createEmployeeService.Execute(parameters));
        }

        [TestMethod]
        public void CreateEmployee_Should_Throw_ArgumentException_If_Employee_Already_Exists()
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

            // Assert
            Assert.ThrowsException<ArgumentException>(() => createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com"
            }));

        }

        [TestMethod]
        public void CreateEmployee_Should_Call_Mapper_Once()
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

            // Assert
            mapperStub.Verify(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>()), Times.Once);
        }

        [TestMethod]
        public void CreateEmployee_Should_Create_New_Employee_If_Correct_Params_Are_Passed()
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

            var employeeExists = dbStub.Employees.SingleOrDefault(e => e.Email == "alex@gmail.com");

            // Assert
            Assert.AreEqual(1, dbStub.Employees.Count());
            Assert.AreEqual("alex@gmail.com", employeeExists.Email);
        }
    }
}
