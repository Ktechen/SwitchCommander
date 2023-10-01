using SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

namespace SwitchCommander.UnitTests.Features.SSH.UpdateSSHCommand;

public class UpdateSSHCommandConfigurationValidatorTest
{
    private readonly UpdateSSHCommandConfigurationValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_CommandMinimumLength_Is_Less_Than_Zero()
    {
        var request = new UpdateSSHCommandConfigurationRequest(Guid.NewGuid(), -1, 0, 0, 0);

        var result = _validator.Validate(request);

        Assert.True(result.Errors.Count > 0);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.CommandMinimumLength));
    }

    [Fact]
    public void Should_Not_Have_Error_When_CommandMinimumLength_Is_Zero()
    {
        var request = new UpdateSSHCommandConfigurationRequest(Guid.NewGuid(), 0, 1, 0, 1);

        var result = _validator.Validate(request);

        Assert.True(result.Errors.Count == 0);
    }

    [Fact]
    public void Should_Have_Error_When_CommandMaximumLength_Is_Less_Than_CommandMinimumLength()
    {
        var request = new UpdateSSHCommandConfigurationRequest(Guid.NewGuid(), 5, 4, 0, 0);

        var result = _validator.Validate(request);

        Assert.True(result.Errors.Count > 0);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.CommandMaximumLength));
    }

    [Fact]
    public void Should_Have_Error_When_DescriptionMinimumLength_Is_Less_Than_Zero()
    {
        var request = new UpdateSSHCommandConfigurationRequest(Guid.NewGuid(), 0, 1, -1, 1);

        var result = _validator.Validate(request);

        Assert.True(result.Errors.Count > 0);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.DescriptionMinimumLength));
    }

    [Fact]
    public void Should_Have_Error_When_DescriptionMaximumLength_Is_Less_Than_DescriptionMinimumLength()
    {
        var request = new UpdateSSHCommandConfigurationRequest(Guid.NewGuid(), 0, 1, 5, 4);

        var result = _validator.Validate(request);

        Assert.True(result.Errors.Count > 0);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.DescriptionMaximumLength));
    }
}