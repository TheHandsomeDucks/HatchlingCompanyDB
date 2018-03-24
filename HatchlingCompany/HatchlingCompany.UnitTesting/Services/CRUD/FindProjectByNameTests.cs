using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class FindProjectByNameTests
    {
        private CreateProject createProjectService;
        private FindProjectByName findProjectByNameService;
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
            this.findProjectByNameService = new FindProjectByName(dbStub, writerStub.Object);
        }

        [TestMethod]
        public void FindProjectByName_Should_Throw_ArgumentException_If_No_Name_Is_Passed()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findProjectByName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => findProjectByNameService.Execute(parameters));
        }

        [TestMethod]
        public void FindProjectByName_Should_Throw_ArgumentException_If_No_Name_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findProjectByName", null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => findProjectByNameService.Execute(parameters));
        }

        [TestMethod]
        public void FindProjectByName_Should_Throw_ArgumentException_If_No_Name_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findProjectByName", ""
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => findProjectByNameService.Execute(parameters));
        }

        [TestMethod]
        public void FindProjectByName_Should_Throw_ArgumentException_If_No_Email_Is_WhiteSpace()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findProjectByName",
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => findProjectByNameService.Execute(parameters));
        }

        [TestMethod]
        public void FindProjectByName_Should_Throw_ArgumentNullException_If_Project_With_Passed_Name_Does_Not_Exists()
        {
            // Arrange
            var parameters = new List<string>()
            {
                "findProjectByName", "noneExistingName"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => findProjectByNameService.Execute(parameters));
        }

        [TestMethod]
        public void FindProjectByName_Should_Return_Project_TypeOf_ListEmployeeDetailsModel_If_Project_Exists()
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

            var projectExists = this.dbStub
                             .Projects
                             .Where(p => p.Name == "TestProject")
                             .ProjectTo<ListProjectDetailsModel>()
                             .SingleOrDefault();

            // Assert
            Assert.IsInstanceOfType(projectExists, typeof(ListProjectDetailsModel));
        }

        [TestMethod]
        public void FindProjectByName_Should_Return_Concrete_Project_If_Existing_Name_Is_Passed()
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

            var projectExists = this.dbStub
                             .Projects
                             .Where(p => p.Name == "TestProject")
                             .ProjectTo<ListProjectDetailsModel>()
                             .SingleOrDefault();

            // Assert
            Assert.AreEqual("TestProject", projectExists.Name);
        }

        [TestMethod]
        public void FindProjectByName_Should_Return_Employee_TypeOf_ListProjectDetailsModel_With_Correcttly_Mapped_Props_If_Project_Exists()
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

            var projectExists = this.dbStub
                             .Projects
                             .Where(p => p.Name == "TestProject")
                             .ProjectTo<ListProjectDetailsModel>()
                             .SingleOrDefault();

            // Assert
            Assert.AreEqual("TestProject", projectExists.Name);
        }
    }
}
