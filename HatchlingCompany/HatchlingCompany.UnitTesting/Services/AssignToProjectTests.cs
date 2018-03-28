using AutoMapper;
using HatchlingCompany.Core.Commands.Implementations;
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
    public class AssignToProjectTests
    {
        private IDbContext dbStub;
        private Mock<IWriter> writerStub;
        private AssignToProject assignToProjectMock;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.writerStub = new Mock<IWriter>();
            this.assignToProjectMock = new AssignToProject(this.dbStub, this.writerStub.Object);
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_DbContext_IsNull()
        {
            // Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AssignToProject(null, this.writerStub.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Writer_IsNull()
        {
            // Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AssignToProject(this.dbStub, null));
        }

        [TestMethod]
        public void AssignToProject_Should_Throw_ArgumentNullException_When_Parameters_AreNull()
        {
            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => this.assignToProjectMock.Execute(null));
        }

        [TestMethod]
        public void AssignToProject_Should_Throw_ArgumentNullException_When_Parameters_AreInvalid()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "assignToProject",
                "employeeEmail",
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => assignToProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void AssignToProject_Should_Throw_ArgumentException_When_EmployeeEmail_IsNull()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "assignToProject",
                null,
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => assignToProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void AssignToProject_Should_Throw_ArgumentException_When_EmployeeEmail_IsWhiteSpace()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "assignToProject",
                " ",
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => assignToProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void AssignToProject_Should_Throw_ArgumentException_When_ProjectName_IsNull()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "assignToProject",
                "employeeEmail",
                null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => assignToProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void AssignToProject_Should_Throw_ArgumentException_When_ProjectName_IsWhiteSpace()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "assignToProject",
                "employeeEmail",
                " "
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => assignToProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void AssignToProject_Should_Throw_ArgumentNullException_When_Employee_IsNotFound()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "assignToProject",
                "employeeEmail",
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => assignToProjectMock.Execute(parameters));
        }

        [TestMethod]
        public void AssignToProject_Should_Throw_ArgumentNullException_When_Project_IsNotFound()
        {
            // Arrange
            this.dbStub.Employees.Add(new Employee() {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "employeeEmail",
                PhoneNumber = "123456789"
                });

            var parameters = new List<string>()
            {
                "assignToProject",
                "employeeEmail",
                "projectName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => assignToProjectMock.Execute(parameters));
        }
    }
}
