﻿using AutoMapper;
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
        [ClassInitialize]
        public static void InitilizeAutomapper(TestContext context)
        {
            AutomapperConfig.Initialize();
        }

        [TestMethod]
        public void ListEmployees_Should_Call_PrintInfo_Of_All_Employee()
        {
            // Arrange 
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var writerMock = new Mock<IWriter>();
            var mapperMock = new Mock<IMapper>();

            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = "alex@gmail.com"
            };

            mapperMock.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            var createEmployeeService = new CreateEmployee(dbMock, writerMock.Object, mapperMock.Object);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com"
            });

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "John", "Dow", "john@gmail.com"
            });


            var listEmployeesService = new ListEmployees(dbMock, writerMock.Object);

            // Act
            listEmployeesService.Execute(new List<string>()
            {
                "listemployees"
            });

            var employees = dbMock.Employees.ToList();

            // Assert
            Assert.IsNotNull(employees);
            Assert.AreEqual("Alex", employees[0].FirstName);
            Assert.AreEqual("Alexov", employees[0].LastName);
            Assert.AreEqual("alex@gmail.com", employees[0].Email);
            Assert.AreEqual("John", employees[1].FirstName);
            Assert.AreEqual("Dow", employees[1].LastName);
            Assert.AreEqual("john@gmail.com", employees[1].Email);
        }
    }
}
