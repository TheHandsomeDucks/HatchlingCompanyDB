using HatchlingCompany.Core;
using HatchlingCompany.Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace HatchlingCompany.UnitTesting
{
    [TestClass]
    public class EngineTests
    {
        private Mock<ICommandParser> commandParserStub;
        private Mock<IReader> readerStub;
        private Mock<IWriter> writerStub;
        private Engine engine;
        private Mock<Engine> engineMock;
        private string commandLine;

        [TestInitialize]
        public void TestInitialize()
        {
            this.writerStub = new Mock<IWriter>();
            this.readerStub = new Mock<IReader>();
            this.commandParserStub = new Mock<ICommandParser>();
            this.engine = new Engine(commandParserStub.Object, readerStub.Object, writerStub.Object);
            this.engineMock = new Mock<Engine>(commandParserStub.Object, readerStub.Object, writerStub.Object);
            this.commandLine = "help";
        }


        [TestMethod]
        public void Engine_Should_Throw_ArgumentNullException_If_Injected_commandParser_Is_Null()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(null, readerStub.Object, writerStub.Object));
        }

        [TestMethod]
        public void Engine_Should_Throw_ArgumentNullException_If_Injected_Reader_Is_Null()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(commandParserStub.Object, null, writerStub.Object));
        }

        [TestMethod]
        public void Engine_Should_Throw_ArgumentNullException_If_Injected_Writer_Is_Null()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Engine(commandParserStub.Object, readerStub.Object, null));
        }

        [TestMethod]
        public void Engine_Should_Throw_ArgumentNullException_If_CommandLine_Is_Null_Empty_Or_WhiteSpace()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => this.engine.Start());
        }

        //[TestMethod]
        //public void Engine_Should_Call_Writer_WriteLine_Once()
        //{
        //    // Arrange & Act
        //    this.engineMock.Setup(x => x.Start()).Verifiable();

        //    // Assert
        //    writerStub.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once());
        //}

        //[TestMethod]
        //public void Engine_Should_Call_Reader_ReadLine_Once()
        //{
        //    // Arrange & Act
        //    this.engine.Start();

        //    // Assert
        //    readerStub.Verify(x => x.ReadLine(), Times.Once());
        //}
    }
}
