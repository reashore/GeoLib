using GeoLib.Contracts;
using GeoLib.Data.Entities;
using GeoLib.Data.Interfaces;
using GeoLib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GeoLib.Tests
{
    [TestClass]
    public class GeoServiceTests
    {
        [TestMethod]
        public void ZipCodeRetrievalTest()
        {
            // Arrange
            Mock<IZipCodeRepository> zipCodeRepositoryMock = new Mock<IZipCodeRepository>();
            ZipCode zipCode = new ZipCode
            {
                City = "LINCOLN PARK",
                State = new State {Abbreviation = "NJ"},
                Zip = "07035"
            };
            zipCodeRepositoryMock.Setup(obj => obj.GetByZipCode("07035")).Returns(zipCode);
            IGeoService geoService = new GeoService(zipCodeRepositoryMock.Object);

            // Act
            ZipCodeData zipCodeData = geoService.GetZipCodeInfo("07035");

            // Assert
            Assert.IsTrue(zipCodeData.City.ToUpper() == "LINCOLN PARK");
            Assert.IsTrue(zipCodeData.State == "NJ");
        }
    }

    // todo: write additional tests
}
