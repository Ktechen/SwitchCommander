using MediatR;

namespace SwitchCommander.Application.Features.UserFeatures.CreateUser.Records;

public sealed record CreateUserRequest(string? Email, string? Name) : IRequest<CreateUserResponse>;