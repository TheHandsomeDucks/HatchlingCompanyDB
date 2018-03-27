using AutoMapper;
using AutoMapper.QueryableExtensions;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.Listing
{
    [TestClass]
    public class ListProjectDetailsTests
    {
        private CreateProject createProjectService;
        private ListProjectDetails listProjectDetailsService;
        private Mock<IMapper> mapperStub;
        private Mock<IWriter> writerStub;
        private IDbContext dbStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.writerStub = new Mock<IWriter>();
            this.mapperStub = new Mock<IMapper>();
            this.createProjectService = new CreateProject(dbStub, writerStub.Object, mapperStub.Object);
            this.listProjectDetailsService = new ListProjectDetails(dbStub, writerStub.Object);
        }

        [TestMethod]
        public void ListProjectDetails_Should_Throw_ArgumentNullException_If_FirstParameter_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                null, "TestProject"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listProjectDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListProjectDetails_Should_Throw_ArgumentNullException_If_FirstParameter_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "", "TestProject"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listProjectDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListProjectDetails_Should_Throw_ArgumentNullException_If_SecondtParameter_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "listProjectDetails", null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listProjectDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListProjectDetails_Should_Throw_ArgumentNullException_If_SecondParameter_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "listProjectDetails", ""
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listProjectDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListProjectDetails_Should_Throw_ArgumentNullException_If_Project_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "listProjectDetails", "NoneExistingProject"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listProjectDetailsService.Execute(parameters));
        }

        [TestMethod]
        public void ListProjectDetails_Should_Should_Find_Concrete_Project()
        {
            var projectToReturn = new Project
            {
                Name = "TestProject"
            };

            mapperStub.Setup(x => x.Map<Project>(It.IsAny<CreateProjectModel>())).Returns(projectToReturn);

            createProjectService.Execute(new List<string>()
            {
                "createProject", "TestProject"
            });

            // Act
            listProjectDetailsService.Execute(new List<string>()
            {
                "listProjectDetails", "TestProject"
            });

            var project = this.dbStub
                        .Projects
                        .Where(e => e.Name == "TestProject")
                        .ProjectTo<ListProjectDetailsModel>()
                        .SingleOrDefault();

            // Assert
            Assert.AreEqual("TestProject", project.Name);
        }
    }
}
