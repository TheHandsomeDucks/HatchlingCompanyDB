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
            Assert.ThrowsException<DbEntityValidationException>(() => createEmployeeService.Execute(parameters));
        }

        [TestMethod]
        public void CreateEmployee_Should_Create_New_Employee()
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
    }
}
