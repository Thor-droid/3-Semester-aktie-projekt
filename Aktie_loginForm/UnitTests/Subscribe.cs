using Aktie_WebAPI.BusinessLogic;
using Aktie_WebAPI.Controllers;
using Aktie_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AbonnementControllerTests
{
    [Fact]
    public void Subscribe_ReturnererOk_NaarTilmeldingLykkes()
    {
        // Arrange
        var serviceMock = new Mock<AbonnementLogic>(null);

        serviceMock
            .Setup(s => s.Subscribe(17, 1, 1))
            .Returns(true);

        var controller = new AbonnementController(serviceMock.Object);

        // Act
        var result = controller.Subscribe(17, 1, 1);

        // Assert
        var okResult = Assert.IsType<ActionResult<ApiResponse>>(result);
        Assert.NotNull(okResult.Value);
        Assert.True(okResult.Value.Success);
    }
}