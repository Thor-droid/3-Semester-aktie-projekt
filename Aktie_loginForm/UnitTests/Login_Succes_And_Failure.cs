using Aktie_WebAPI.Controllers;
using Aktie_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AuthControllerTests
{
    [Fact]
    public void Login_ShouldReturnOk_WhenCredentialsAreCorrect()
    {
        // Arrange
        var controller = new AuthController();

        var model = new LoginModel
        {
            Email = "existing@test.com",
            Password = "correctpassword"
        };

        // Act
        var result = controller.Login(model);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
    [Fact]
    public void Login_ShouldReturnUnauthorized_WhenCredentialsAreWrong()
    {
        var controller = new AuthController();

        var model = new LoginModel
        {
            Email = "wrong@test.com",
            Password = "wrong"
        };

        var result = controller.Login(model);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}