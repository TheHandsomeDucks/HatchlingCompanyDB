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
        //[ClassInitialize]
        //public static void InitilizeAutomapper(TestContext context)
        //{
        //    AutomapperConfig.Initialize();
        //}

        [TestMethod]
        public void ListEmployeeDetails_Should_Call_Concret_Employee()
        {
            // Arragne
            //AutomapperConfig.Initialize();
            
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var writerMock = new Mock<IWriter>();
            var mapperMock = new Mock<IMapper>();

            var employeeToReturn = new Employee
            {
                FirstName = "Ace",
                LastName = "Base",
                Email = "ace@gmail.com"
            };

            mapperMock.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);

            var createEmployeeService = new CreateEmployee(dbMock, writerMock.Object, mapperMock.Object);

            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Ace", "Base", "ace@gmail.com"
            });

            var listEmployeeDetailsService = new ListEmployeeDetails(dbMock, writerMock.Object);

            // Act
            listEmployeeDetailsService.Execute(new List<string>()
            {
                "listemployeedetails", "ace@gmail.com"
            });

            var employeeExists = dbMock.Employees.SingleOrDefault(e => e.Email == "ace@gmail.com");

            // Assert
            Assert.AreEqual("Ace", employeeExists.FirstName);
            Assert.AreEqual("Base", employeeExists.LastName);
            Assert.AreEqual("ace@gmail.com", employeeExists.Email);
        }
    }
}
