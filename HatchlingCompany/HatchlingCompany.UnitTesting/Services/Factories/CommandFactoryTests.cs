using Autofac;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Factories.Implementations;
using HatchlingCompany.Core.Services.Listing;
using HatchlingCompany.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;


namespace HatchlingCompany.UnitTesting.Services.Factories
{
    [TestClass]
    public class CommandFactoryTests
    {
        private ListEmployees listEmployeesService;
        private Mock<IWriter> writerStub;
        private IDbContext dbStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.writerStub = new Mock<IWriter>();
            this.listEmployeesService = new ListEmployees(dbStub, writerStub.Object);
        }

        [TestMethod]
        public void CommandFactory_Should_Throw_ArgumentNullException_If_Injected_Container_Is_Null()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CommandFactory(null));
        }

        [TestMethod]
        public void CommandFactory_CreateCommand_Should_Throw_ArgumentNullException_If_Injected_Container_Is_Null()
        {
            // Arrange
            var containerStub = new Mock<IComponentContext>();
            var commandFactory = new CommandFactory(containerStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => commandFactory.CreateCommand(null));
        }

        //[TestMethod]
        //public void CommandFactory_CreateCommand_Should_Call_Container_ResolveNamed_Once()
        //{
        //    // Arrange
        //    var containerStub = new Mock<IComponentContext>(); ;
        //    containerStub.Setup(x => x.ResolveNamed<ICommand>(It.IsAny<string>())).Returns(listEmployeesService);

        //    var commandFactoryMock = new Mock<CommandFactory>(containerStub.Object);

        //    // Act
        //    var listEmployees = commandFactoryMock.Object.CreateCommand("listemployees");

        //    // Assert
        //    containerStub.Verify(x => x.ResolveNamed<ICommand>(It.IsAny<string>()), Times.Once);
        //}

        //[TestMethod]
        //public void CommandFactory_CreateCommand_Should_Call_Container_ResolveNamed_If_Passed_Name_Is_Valid()
        //{
        //    // Arrange
        //    var containerStub = new Mock<IComponentContext>(); ;
        //    containerStub.Setup(x => x.ResolveNamed<ICommand>(It.IsAny<string>())).Returns(listEmployeesService);

        //    var commandFactoryMock = new Mock<CommandFactory>(containerStub.Object);

        //    // Act
        //    var listEmployees = commandFactoryMock.Object.CreateCommand("listemployees");

        //    // Assert
        //    Assert.IsInstanceOfType(listEmployees, typeof(ICommand));
        //}
    }
}
