using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.Exporting;
using HatchlingCompany.Data;
using HatchlingCompany.Utils.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatchlingCompany.UnitTesting.Services.Exporting
{
    [TestClass]
    public class JSONExportEmployeesTests
    {
        private HatchlingCompanyDbContext dbStub;
        private Mock<ISerializer> serializerStub;
        private Mock<IExporter> exporterStub;
        private Mock<IWriter> writerStub;
        private object JSONExportEmployeesMock;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.serializerStub = new Mock<ISerializer>();
            this.exporterStub = new Mock<IExporter>();
            this.writerStub = new Mock<IWriter>();
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_DbContext_IsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new JSONExportEmployees(null, this.serializerStub.Object, this.exporterStub.Object, this.writerStub.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Serializer_IsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new JSONExportEmployees(this.dbStub, null, this.exporterStub.Object, this.writerStub.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Exporter_IsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new JSONExportEmployees(this.dbStub, this.serializerStub.Object, null, this.writerStub.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Writer_IsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new JSONExportEmployees(this.dbStub, this.serializerStub.Object, this.exporterStub.Object, null));
        }

        [TestMethod]
        public void Execute_Should_Throw_ArgumentNullException_When_Parameters_IsNull()
        {
            // Arrange
            var JSONExportEmployeesMock = new JSONExportEmployees(this.dbStub, this.serializerStub.Object, this.exporterStub.Object, this.writerStub.Object);

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => JSONExportEmployeesMock.Execute(null));
        }

        [TestMethod]
        public void Execute_Should_Throw_ArgumentNullException_When_ParameterCount_IsNotValid()
        {
            // Arrange
            var JSONExportEmployeesMock = new JSONExportEmployees(this.dbStub, this.serializerStub.Object, this.exporterStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                "JSONExportEmployees"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => JSONExportEmployeesMock.Execute(parameters));
        }

        [TestMethod]
        public void Execute_Should_Throw_ArgumentNullException_When_Command_IsNull()
        {
            // Arrange
            var JSONExportEmployeesMock = new JSONExportEmployees(this.dbStub, this.serializerStub.Object, this.exporterStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                null,
                "all"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => JSONExportEmployeesMock.Execute(parameters));
        }

        [TestMethod]
        public void Execute_Should_Throw_ArgumentNullException_When_Command_IsWhitespace()
        {
            // Arrange
            var JSONExportEmployeesMock = new JSONExportEmployees(this.dbStub, this.serializerStub.Object, this.exporterStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                " ",
                "all"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => JSONExportEmployeesMock.Execute(parameters));
        }

        [TestMethod]
        public void Execute_Should_Call_Serialize_Once()
        {
            // Arrange
            this.serializerStub.Setup(x => x.Serialize(It.IsAny<IList<ListEmployeeDetailsModel>>()));

            var JSONExportEmployeesMock = new JSONExportEmployees(this.dbStub, this.serializerStub.Object, this.exporterStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                "JSONExportEmployees",
                "all"
            };

            // Act
            JSONExportEmployeesMock.Execute(parameters);

            // Assert
            this.serializerStub.Verify(x => x.Serialize(It.IsAny<IList<ListEmployeeDetailsModel>>()), Times.Once);
        }

        [TestMethod]
        public void Execute_Should_Call_Export_Once()
        {
            // Arrange
            this.exporterStub.Setup(x => x.Export(It.IsAny<string>(), It.IsAny<string>()));

            var JSONExportEmployeesMock = new JSONExportEmployees(this.dbStub, this.serializerStub.Object, this.exporterStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                "JSONExportEmployees",
                "all"
            };

            // Act
            JSONExportEmployeesMock.Execute(parameters);

            // Assert
            this.exporterStub.Verify(x => x.Export(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
