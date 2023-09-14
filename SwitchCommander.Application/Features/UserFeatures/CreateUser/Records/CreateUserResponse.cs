namespace SwitchCommander.Application.Features.UserFeatures.CreateUser.Records;

public sealed record CreateUserResponse(Guid id, string? Email, string? Name);