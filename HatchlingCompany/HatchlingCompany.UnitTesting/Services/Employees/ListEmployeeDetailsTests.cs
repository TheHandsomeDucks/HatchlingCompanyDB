using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
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
        [TestMethod]
        public void ListEmployeeDetails_Should_Call_PrintInfo_Of_Concret_Employee()
        {
            // Arrange 
            // create employee
            var writerMock = new Mock<IWriter>();
            AutoMapperProfile.Initialize();
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var createEmployeeService = new CreateEmployee(dbMock, writerMock.Object);

            var createParams = new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com", "phone"
            };
            createEmployeeService.Execute(createParams);

            // create listEmployeeDetails
            var listEmployeeDetailsService = new ListEmployeeDetails(dbMock, writerMock.Object);
            var listDetailsParamas = new List<string>()
            {
                "listemployeedetails", "alex@gmail.com"
            };

            // Act
            listEmployeeDetailsService.Execute(listDetailsParamas);
            var employee = dbMock.Employees.SingleOrDefault(x => x.Email == "alex@gmail.com");

            // Assert
            Assert.IsInstanceOfType(employee, typeof(Employee));
        }


    }
}
