using Moq;
using SwitchCommander.Application.Features.User.CreateUser;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.UnitTests.Features.User;

public class CreateUserValidatorTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly CreateUserValidator _validator;

    public CreateUserValidatorTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _validator = new CreateUserValidator(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Have_Errors_When_Name_Is_Empty()
    {
        var createUserRequest = new CreateUserRequest("test@test.com", "", "NameTwo");

        var result = await _validator.ValidateAsync(createUserRequest);

        Assert.True(result.Errors.Count >= 1);
        Assert.True(result.Errors.First().PropertyName == "Name");
    }

    [Fact]
    public async Task Should_Have_Errors_When_Email_Is_Empty()
    {
        var createUserRequest = new CreateUserRequest("", "Name", "NameTwo");

        var result = await _validator.ValidateAsync(createUserRequest);

        Assert.True(result.Errors.Count >= 1);
        Assert.True(result.Errors.First().PropertyName == "Email");
    }
}