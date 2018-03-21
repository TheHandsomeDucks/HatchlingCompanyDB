using AutoMapper;
using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Employees
{
    [TestClass]
    public class CreateEmployeeTests
    {
        [TestMethod]
        public void CreateEmployee_Should_Throw_DbEntityValidationException_If_Email_Is_Null()
        {
            // Arragne
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var writerMock = new Mock<IWriter>();
            var mapperMock = new Mock<IMapper>();

            var employeeToReturn = new Employee
            {
                FirstName = "Alex",
                LastName = "Alexov",
                Email = null
            };


            mapperMock.Setup(x => x.Map<Employee>(It.IsAny<CreateEmployeeModel>())).Returns(employeeToReturn);


            var createEmployeeService = new CreateEmployee(dbMock, writerMock.Object, mapperMock.Object);

            //Act
            var parameters = new List<string>()
            {
                "createEmployee", "Alex", "Alexov", null
            };

            // Assert
            Assert.ThrowsException<DbEntityValidationException>(() => createEmployeeService.Execute(parameters));
        }

        [TestMethod]
        public void CreateEmployee_Should_Create_New_Employee()
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

            //Act
            createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com"
            });

            var employeeExists = dbMock.Employees.SingleOrDefault(e => e.Email == "alex@gmail.com");

            // Assert
            Assert.AreEqual(1, dbMock.Employees.Count());
            Assert.AreEqual("alex@gmail.com", employeeExists.Email);
        }
    }
}
