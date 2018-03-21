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
    public class ListEmployeeDetailsTests
    {
        [ClassInitialize]
        public static void InitilizeAutomapper(TestContext context)
        {
            AutomapperConfig.Initialize();
        }

        [TestMethod]
        public void ListEmployeeDetails_Should_Call_Concret_Employee()
        {
            // Arragne
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

            var listEmployeeDetailsService = new ListEmployeeDetails(dbMock, writerMock.Object);

            // Act
            listEmployeeDetailsService.Execute(new List<string>()
            {
                "listemployeedetails", "alex@gmail.com"
            });

            var employeeExists = dbMock.Employees.SingleOrDefault(e => e.Email == "alex@gmail.com");

            // Assert
            Assert.AreEqual("Alex", employeeExists.FirstName);
            Assert.AreEqual("Alexov", employeeExists.LastName);
            Assert.AreEqual("alex@gmail.com", employeeExists.Email);
        }
    }
}
