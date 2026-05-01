using Aktie_WebAPI.BusinessLogic;
using Aktie_WebAPI.DatabaseAccess;
using Moq;
using Xunit;

namespace Tests
{
    public class AbonnementServiceTests
    {
        [Fact]
        public void Subscribe_ReturnererFalse_NaarPakkeErFuld()
        {
            // Arrange
            var repoMock = new Mock<AbonnementRepository>(null);

            repoMock
                .Setup(r => r.Subscribe(17, 1, 1))
                .Returns(false); // simuler "fuld pakke"

            var service = new AbonnementService(repoMock.Object);

            // Act
            var result = service.Subscribe(17, 1, 1);

            // Assert
            Assert.False(result);
        }
    }
}