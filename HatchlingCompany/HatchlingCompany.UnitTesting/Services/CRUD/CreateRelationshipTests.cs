using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HatchlingCompany.UnitTesting.Services.CRUD
{
    [TestClass]
    public class CreateRelationshipTests
    {
        private CreateRelationship createRelationshipService;
        private Mock<IMapper> mapperStub;
        private Mock<IWriter> writerStub;
        private IDbContext dbStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.writerStub = new Mock<IWriter>();
            this.mapperStub = new Mock<IMapper>();
            this.createRelationshipService = new CreateRelationship(dbStub, writerStub.Object, mapperStub.Object);
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_No_Params_Are_Passed()
        {
            var parameters = new List<string>()
            {
                "createRelationship"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Wrong_Number_Of_Params_Is_Passed()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "aaaaaa"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_First_Email_Is_Null()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                null,
                "pesho@goshov.com",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_First_Email_Is_Empty()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                String.Empty,
                "pesho@goshov.com",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_First_Email_Is_Whitespace()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "   ",
                "pesho@goshov.com",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_First_Email_Is_Not_Email()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "peshopeshopesho",
                "pesho@goshov.com",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Second_Email_Is_Null()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                null,
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Second_Email_Is_Empty()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                String.Empty,
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Second_Email_Is_Whitespace()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "   ",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Second_Email_Is_Not_Email()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "peshopeshopesho",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Strength_Is_Not_Integer()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "gosho@peshov.com",
                "test"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Strength_Is_Negative()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "gosho@peshov.com",
                "-5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Strength_Is_Above_Nine()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "gosho@peshov.com",
                "10"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Emails_Are_Equal()
        {
            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "pesho@goshov.com",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_First_Employee_Not_In_Db()
        {
            var employee = new Employee()
            {
                Email = "gosho@goshov.com"
            };

            this.dbStub.Employees.Add(employee);

            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "gosho@goshov.com",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Second_Employee_Not_In_Db()
        {
            var employee = new Employee()
            {
                Email = "pesho@goshov.com"
            };

            this.dbStub.Employees.Add(employee);

            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "gosho@goshov.com",
                "5"
            };

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Throw_ArgumentException_When_Relationship_Exists_In_Db()
        {
            var firstEmployee = new Employee()
            {
                Id = 1,
                Email = "pesho@goshov.com"
            };

            var secondEmployee = new Employee()
            {
                Id = 2,
                Email = "gosho@goshov.com"
            };

            var parameters = new List<string>()
            {
                "createRelationship",
                "pesho@goshov.com",
                "gosho@goshov.com",
                "5"
            };

            var relationship = new Relationship()
            {
                FirstEmployeeId = 1,
                FirstEmployee = firstEmployee,
                SecondEmployeeId = 2,
                SecondEmployee = secondEmployee,
                RelationshipStrength = 5
            };

            this.mapperStub.Setup(x => x.Map<Relationship>(It.IsAny<CreateRelationshipModel>())).Returns(relationship);

            this.dbStub.Employees.Add(firstEmployee);
            this.dbStub.Employees.Add(secondEmployee);
            this.dbStub.Relationships.Add(relationship);
            this.dbStub.SaveChanges();

            Assert.ThrowsException<ArgumentException>(() => this.createRelationshipService.Execute(parameters));
        }

        [TestMethod]
        public void CreateRelationship_Should_Add_Correct_Relationship_In_Db()
        {
            var firstEmployee = new Employee()
            {
                Id = 1,
                Email = "pesho@goshov.com"
            };

            var secondEmployee = new Employee()
            {
                Id = 2,
                Email = "gosho@goshov.com"
            };

            var parameters = new List<string>()
                {
                    "createRelationship",
                    "pesho@goshov.com",
                    "gosho@goshov.com",
                    "5"
                };

            var relationship = new Relationship()
            {
                FirstEmployeeId = 1,
                FirstEmployee = firstEmployee,
                SecondEmployeeId = 2,
                SecondEmployee = secondEmployee,
                RelationshipStrength = 5
            };

            this.mapperStub.Setup(x => x.Map<Relationship>(It.IsAny<CreateRelationshipModel>())).Returns(relationship);

            this.dbStub.Employees.Add(firstEmployee);
            this.dbStub.Employees.Add(secondEmployee);
            this.dbStub.SaveChanges();

            this.createRelationshipService.Execute(parameters);

            var fromDb = this.dbStub.Relationships.First();
            Assert.AreEqual(relationship, fromDb);
        }
    }
}
