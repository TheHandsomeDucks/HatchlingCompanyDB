using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Services;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace HatchlingCompany.UnitTesting.Services
{
    [TestClass]
    public class RemoveFromProjectTests
    {
        private IDbContext dbStub;
        private Mock<IWriter> writerStub;
        private RemoveFromProject removeFromProjectMock;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.writerStub = new Mock<IWriter>();
            this.removeFromProjectMock = new RemoveFromProject(this.dbStub, this.writerStub.Object);
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_DbContext_IsNull()
        {
            // Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new RemoveFromProject(null, this.writerStub.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Writer_IsNull()
        {
            // Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new RemoveFromProject(this.dbStub, null));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentNullException_When_Parameters_AreNull()
        {
            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => this.removeFromProjectMock.Execute(null));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentNullException_When_Parameters_AreInvalid()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "removeFromProject",
                "employeeEmail",
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => removeFromProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentException_When_CommandName_IsNull()
        {
            // Arrange
            var parameters = new List<string>()
            {
                null,
                "employeeEmail",
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => removeFromProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentException_When_CommandName_IsWhitespace()
        {
            // Arrange
            var parameters = new List<string>()
            {
                " ",
                "employeeEmail",
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => removeFromProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentException_When_EmployeeEmail_IsNull()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "removeFromProject",
                null,
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => removeFromProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentException_When_EmployeeEmail_IsWhiteSpace()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "removeFromProject",
                " ",
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => removeFromProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentException_When_ProjectName_IsNull()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "removeFromProject",
                "employeeEmail",
                null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => removeFromProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentException_When_ProjectName_IsWhiteSpace()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "removeFromProject",
                "employeeEmail",
                " "
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => removeFromProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentNullException_When_Employee_IsNotFound()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "removeFromProject",
                "employeeEmail",
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => removeFromProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void RemoveFromProject_Should_Throw_ArgumentNullException_When_Project_IsNotFound()
        {
            // Arrange
            this.dbStub.Employees.Add(new Employee()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "employeeEmail",
                PhoneNumber = "123456789"
            });

            var parameters = new List<string>()
            {
                "removeFromProject",
                "employeeEmail",
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => removeFromProjectMock.Execute(parameters));
        }
    }
}
