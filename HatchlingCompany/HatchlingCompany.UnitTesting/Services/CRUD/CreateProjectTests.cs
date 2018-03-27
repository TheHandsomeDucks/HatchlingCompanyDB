using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HatchlingCompany.UnitTesting.Services.CRUD
{
    [TestClass]
    public class CreateProjectTests
    {
        private CreateProject createProjectService;
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
        }

        [TestMethod]
        public void CreateProject_Should_Throw_ArgumentException_If_No_Params_Are_Passed()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "createProject"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => createProjectService.Execute(parameters));
        }

        [TestMethod]
        public void CreateProject_Should_Throw_ArgumentException_If_Name_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "createProject", null
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => createProjectService.Execute(parameters));
        }

        [TestMethod]
        public void CreateProject_Should_Throw_ArgumentException_If_Project_Already_Exists()
        {
            // Arrange
            var projectToReturn = new Project
            {
                Name = "TestProject"
            };

            mapperStub.Setup(x => x.Map<Project>(It.IsAny<CreateProjectModel>())).Returns(projectToReturn);

            // Act
            createProjectService.Execute(new List<string>()
            {
                "createProject", "TestProject"
            });

            // Assert
            Assert.ThrowsException<ArgumentException>(() => createProjectService.Execute(new List<string>()
            {
                "createProject", "TestProject"
            }));

        }

        [TestMethod]
        public void CreateProject_Should_Call_Mapper_Once()
        {
            // Arrange
            var projectToReturn = new Project
            {
                Name = "TestProject"
            };

            mapperStub.Setup(x => x.Map<Project>(It.IsAny<CreateProjectModel>())).Returns(projectToReturn);

            // Act
            createProjectService.Execute(new List<string>()
            {
                "createProject", "TestProject"
            });

            // Assert
            mapperStub.Verify(x => x.Map<Project>(It.IsAny<CreateProjectModel>()), Times.Once);
        }

        [TestMethod]
        public void CreateProject_Should_Create_New_Project_If_Correct_Params_Are_Passed()
        {
            // Arrange
            var projectToReturn = new Project
            {
                Name = "TestProject"
            };

            mapperStub.Setup(x => x.Map<Project>(It.IsAny<CreateProjectModel>())).Returns(projectToReturn);

            // Act
            createProjectService.Execute(new List<string>()
            {
                "createProject", "TestProject"
            });

            var projectExists = dbStub.Projects.SingleOrDefault(e => e.Name == "TestProject");

            // Assert
            Assert.AreEqual(1, dbStub.Projects.Count());
            Assert.AreEqual("TestProject", projectExists.Name);
        }
    }
}
