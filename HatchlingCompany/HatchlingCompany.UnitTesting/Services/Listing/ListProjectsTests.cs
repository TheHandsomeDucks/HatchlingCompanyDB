using System;
using System.Collections.Generic;
using AutoMapper;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Services.CRUD;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        public void ListEmployees_Should_Throw_ArgumentNullException_If_Parameter_Is_Null()
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
        public void ListEmployees_Should_Throw_ArgumentNullException_If_Parameter_Is_EmptyString()
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
        public void ListEmployeesShould_Return_ProjectList_TypeOf_ListEmployeesModel_If_Prpjects_Are_Found()
        {
            // Arrange
            var parameters = new List<string>()
            {
                null
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => listProjectsService.Execute(parameters));
        }

    }
}
