using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Models;
using HatchlingCompany.Core.Services.Exporting;
using HatchlingCompany.Data;
using HatchlingCompany.Models;
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
    public class JSONImportsEmployeesTests
    {
        private HatchlingCompanyDbContext dbStub;
        private Mock<IDeserializer<IList<Employee>>> deserializerStub;
        private Mock<IImporter> importerStub;
        private Mock<IWriter> writerStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbStub = new HatchlingCompanyDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.deserializerStub = new Mock<IDeserializer<IList<Employee>>>();
            this.importerStub = new Mock<IImporter>();
            this.writerStub = new Mock<IWriter>();
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_DbContext_IsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new JSONImportEmployees(null, this.deserializerStub.Object, this.importerStub.Object, this.writerStub.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Serializer_IsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new JSONImportEmployees(this.dbStub, null, this.importerStub.Object, this.writerStub.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Exporter_IsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, null, this.writerStub.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Writer_IsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, this.importerStub.Object, null));
        }

        [TestMethod]
        public void Execute_Should_Throw_ArgumentNullException_When_Parameters_IsNull()
        {
            // Arrange
            var JSONImportEmployeesMock = new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, this.importerStub.Object, this.writerStub.Object);

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => JSONImportEmployeesMock.Execute(null));
        }

        [TestMethod]
        public void Execute_Should_Throw_ArgumentNullException_When_ParameterCount_IsNotValid()
        {
            // Arrange
            var JSONImportEmployeesMock = new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, this.importerStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                "JSONImportEmployees",
                "unnecessary argument"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => JSONImportEmployeesMock.Execute(parameters));
        }

        [TestMethod]
        public void Execute_Should_Throw_ArgumentNullException_When_Command_IsNull()
        {
            // Arrange
            var JSONImportEmployeesMock = new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, this.importerStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                null,
                "all"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => JSONImportEmployeesMock.Execute(parameters));
        }

        [TestMethod]
        public void Execute_Should_Throw_ArgumentNullException_When_Command_IsWhitespace()
        {
            // Arrange
            var JSONImportEmployeesMock = new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, this.importerStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                " ",
                "all"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => JSONImportEmployeesMock.Execute(parameters));
        }

        [TestMethod]
        public void Execute_Should_Thrown_ArgumentException_When_ThereAreNoEmployees()
        {
            // Arrange
            this.importerStub.Setup(x => x.Import(It.IsAny<string>())).Returns("json");
            this.deserializerStub.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(new List<Employee>());

            var JSONImportEmployeesMock = new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, this.importerStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                "JSONImportEmployees"
            };

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => JSONImportEmployeesMock.Execute(parameters));
        }

        [TestMethod]
        public void Execute_Should_Call_Import_Once()
        {
            // Arrange
            this.importerStub.Setup(x => x.Import(It.IsAny<string>())).Returns("json");
            this.deserializerStub.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(new List<Employee>()
            {
                new Employee() {FirstName="Test", LastName = "Test", Email = "test@test.bg", PhoneNumber = "0891234512"}
            });

            var JSONImportEmployeesMock = new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, this.importerStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                "JSONImportEmployees"
            };

            // Act
            JSONImportEmployeesMock.Execute(parameters);

            // Assert
            this.importerStub.Verify(x => x.Import(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Execute_Should_Call_Deserialize_Once()
        {
            // Arrange
            this.importerStub.Setup(x => x.Import(It.IsAny<string>())).Returns("json");
            this.deserializerStub.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(new List<Employee>()
            {
                new Employee() {FirstName="Test", LastName = "Test", Email = "test@test.bg", PhoneNumber = "0891234512"}
            });

            var JSONImportEmployeesMock = new JSONImportEmployees(this.dbStub, this.deserializerStub.Object, this.importerStub.Object, this.writerStub.Object);

            var parameters = new List<string>()
            {
                "JSONImportEmployees"
            };

            // Act
            JSONImportEmployeesMock.Execute(parameters);

            // Assert
            this.deserializerStub.Verify(x => x.Deserialize(It.IsAny<string>()), Times.Once);
        }
    }
}
