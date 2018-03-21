using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
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
        public void ListEmployeeDetails_Should_Call_FirstName_Of_Concret_Employee()
        {
            // Arrange 
            // create employee
            AutomapperConfig.Initialize();
            var writerMock = new Mock<IWriter>();
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var createEmployeeService = new CreateEmployee(dbMock, writerMock.Object);

            var createParams = new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com", "phone"
            };
            createEmployeeService.Execute(createParams);

            // create listEmployeeDetails
            var listEmployeeDetailsService = new ListEmployeeDetails(dbMock, writerMock.Object);
            var parameters = new List<string>()
            {
                "listemployeedetails", "alex@gmail.com"
            };

            // Act
            listEmployeeDetailsService.Execute(parameters);

            var employee = dbMock.Employees.SingleOrDefault(x => x.Email == "alex@gmail.com");

            // Assert
            Assert.AreEqual(employee.FirstName, "Alex"); // TODO error Message: Test method HatchlingCompany.UnitTesting.Services.Employees.ListEmployeeDetailsTests.ListEmployeeDetails_Should_Call_PrintInfo_Of_Concret_Employee threw exception: 
                                                         //System.IO.FileLoadException: Could not load file or assembly 'System.Data.Common, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies.The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)
        }

    }
}
