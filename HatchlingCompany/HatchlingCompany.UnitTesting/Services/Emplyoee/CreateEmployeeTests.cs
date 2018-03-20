using HatchlingCompany.Core.Commands.Implementations;
using HatchlingCompany.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Emplyoee
{
    [TestClass]
    public class CreateEmployeeTests
    {
        [TestMethod]
        public void CreateEmployee_Should_Throw_DbEntityValidationException_If_Email_Is_Null()
        {
            // Arragne
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var createEmployeeService = new CreateEmployee(dbMock);

            //Act
            var parameters = new List<string>()
            {
                "createEmployee", "Alex", "Alexov", null, "phone"
            };

            // Assert
            Assert.ThrowsException<DbEntityValidationException>(() => createEmployeeService.Execute(parameters));
        }

        [TestMethod]
        public void CreateEmployee_Should_Create_Employee()
        {
            // Arragne
            var dbMock = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            var createEmployeeService = new CreateEmployee(dbMock);

            //Act
            var returnMessage = createEmployeeService.Execute(new List<string>()
            {
                "createEmployee", "Alex", "Alexov", "alex@gmail.com", "phone"
            });

            var employee = dbMock.Employees.SingleOrDefault(e => e.Email == "alex@gmail.com");

            // Assert
            Assert.AreEqual(1, dbMock.Employees.Count());
            Assert.AreEqual("alex@gmail.com", employee.Email);
            Assert.AreEqual(returnMessage, "A new employee with name Alex Alexov was created.");
        }
    }
}
