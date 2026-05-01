using Aktie_WebAPI.BusinessLogic;
using Aktie_WebAPI.Controllers;
using Aktie_WebAPI.DatabaseAccess;
using Aktie_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AuthControllerTests
{
    [Fact]
    public void Login_ShouldReturnValue_WhenCredentialsAreCorrect()
    {
        // Arrange
        var authServiceMock = new Mock<AuthService>((AuthRepository)null!);

        authServiceMock
            .Setup(s => s.Login(It.Is<LoginModel>(m =>
                m.Email == "fisk@fisk.fisk" &&
                m.Password == "fisk"
            )))
            .Returns(new LoginResponse());

        var controller = new AuthController(authServiceMock.Object);

        var model = new LoginModel
        {
            Email = "fisk@fisk.fisk",
            Password = "fisk"
        };

        // Act
        var result = controller.Login(model);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Null(result.Result);
    }

    [Fact]
    public void Login_ShouldReturnUnauthorized_WhenCredentialsAreWrong()
    {
        // Arrange
        var authServiceMock = new Mock<AuthService>((AuthRepository)null!);

        authServiceMock
            .Setup(s => s.Login(It.IsAny<LoginModel>()))
            .Returns((LoginResponse?)null);

        var controller = new AuthController(authServiceMock.Object);

        var model = new LoginModel
        {
            Email = "wrong@test.com",
            Password = "wrong"
        };

        // Act
        var result = controller.Login(model);

        // Assert
        Assert.IsType<UnauthorizedObjectResult>(result.Result);
    }
}