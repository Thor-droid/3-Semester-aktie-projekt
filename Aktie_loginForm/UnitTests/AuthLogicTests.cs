using Aktie_WebAPI.BusinessLogic;
using Aktie_WebAPI.DatabaseAccess;
using Aktie_WebAPI.Model;
using Aktie_WebsiteMVCV2.Models;
using Moq;
using Xunit;

public class AuthLogicTests
{
    [Fact]
    public void GetAllUsers_ReturnsUsersFromAccess()
    {
        // Arrange
        var fakeUsers = new List<UserViewModel>
        {
            new UserViewModel(1, "Lars", "Lars@test.dk"),
            new UserViewModel(2, "Børge", "Børge@test.dk")
        };

        var mockAccess = new Mock<AuthAccess>(null);

        mockAccess
            .Setup(x => x.GetAllUsers())
            .Returns(fakeUsers);

        var logic = new AuthLogic(mockAccess.Object);

        // Act
        var result = logic.GetAllUsers();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Lars", result[0].Navn);
        Assert.Equal("Børge@test.dk", result[1].Email);
    }
}