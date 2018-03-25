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
    public class ListProjectsTests
    {
        private CreateProject createProjectService;
        private ListProjects listProjectsService;
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
            this.listProjectsService = new ListProjects(dbStub, writerStub.Object);
        }

        [TestMethod]
        public void ListProjects_Should_Throw_ArgumentNullException_If_Parameter_Is_Null()
        {
            // Arrange
            var parameters = new List<string>()
            {
                null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listProjectsService.Execute(parameters));
        }

        [TestMethod]
        public void ListProjects_Should_Throw_ArgumentNullException_If_Parameter_Is_EmptyString()
        {
            // Arrange
            var parameters = new List<string>()
            {
                ""
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listProjectsService.Execute(parameters));
        }

        [TestMethod]
        public void ListProjects_Should_Return_ProjectList_TypeOf_ListEmployeesModel_If_Prpjects_Are_Found()
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

            // Act
            listProjectsService.Execute(new List<string>()
            {
                "listEmployees"
            });

            var projects = this.dbStub
                               .Projects
                               .ProjectTo<ListProjectsModel>()
                               .ToList();

            // Assert
            Assert.IsInstanceOfType(projects, typeof(List<ListProjectsModel>));
        }

        [TestMethod]
        public void ListProjects_Should_Throw_ArgumentException_If_EmployeesList_Is_Null()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentException>(() => listProjectsService.Execute(new List<string>()
            {
                "listemployees"
            }));
        }

        [TestMethod]
        public void ListProjects_Should_Call_PrintInfo_Of_All_Projects()
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

            // Act
            listProjectsService.Execute(new List<string>()
            {
                "listEmployees"
            });

            var projects = this.dbStub
                               .Projects
                               .ProjectTo<ListProjectsModel>()
                               .ToList();

            // Assert
            Assert.IsNotNull(projects);
            Assert.AreEqual("TestProject", projects[0].Name);
        }

    }
}
