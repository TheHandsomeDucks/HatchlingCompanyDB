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
    public class ListEmployeesTests
    {
        private CreateEmployee createEmployeeService;
        private ListEmployees listEmployeesService;
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
            this.listEmployeesService = new ListEmployees(dbStub, writerStub.Object);
        }

        [TestMethod]
        public void ListEmployees_Should_Throw_ArgumentNullException_If_Parameter_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeesService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployees_Should_Throw_ArgumentNullException_If_Parameter_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                ""
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listEmployeesService.Execute(parameters));
        }

        [TestMethod]
        public void ListEmployees_Should_Return_EmployeesList_TypeOf_ListEmployeesModel_If_Employees_Exist()
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
            listEmployeesService.Execute(new List<string>()
            {
                "listEmployees"
            });

            var employees = this.dbStub
                            .Employees
                            .ProjectTo<ListEmployeesModel>()
                            .ToList();

            // Assert
            Assert.IsInstanceOfType(employees, typeof(List<ListEmployeesModel>));
        }

        [TestMethod]
        public void ListEmployees_Should_Throw_ArgumentException_If_EmployeesList_Is_Null()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentException>(() => listEmployeesService.Execute(new List<string>()
            {
                "listemployees"
            }));
        }

        [TestMethod]
        public void ListEmployees_Should_Call_PrintInfo_Of_All_Employees()
        {
            // Arrange
            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = "alex@gmail.com"
            };

            mapperStub.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            var createEmployeeService = new CreateEmployee(dbStub, writerStub.Object, mapperStub.Object);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com"
            });

            var listEmployeesService = new ListEmployees(dbStub, writerStub.Object);

            // Act
            listEmployeesService.Execute(new List<string>()
            {
                "listemployees"
            });

            var employees = this.dbStub
                          .Employees
                          .ProjectTo<ListEmployeesModel>()
                          .ToList();

            // Assert
            Assert.IsNotNull(employees);
            Assert.AreEqual("Alex", employees[0].FirstName);
            Assert.AreEqual("Alexov", employees[0].LastName);
            Assert.AreEqual("alex@gmail.com", employees[0].Email);
        }
    }
}
