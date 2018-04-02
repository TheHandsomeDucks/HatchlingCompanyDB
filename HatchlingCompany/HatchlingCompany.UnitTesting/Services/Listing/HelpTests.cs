using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Services.Listing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace HatchlingCompany.UnitTesting.Services.Listing
{
    [TestClass]
    public class HelpTests
    {
        private Help helpService;
        private Mock<IWriter> writerStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.writerStub = new Mock<IWriter>();
            this.helpService = new Help(writerStub.Object);

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
            Assert.ThrowsException<ArgumentNullException>(() => helpService.Execute(parameters));
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
            Assert.ThrowsException<ArgumentNullException>(() => helpService.Execute(parameters));
        }
    }
}
