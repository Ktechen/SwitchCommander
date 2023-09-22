using MediatR;

namespace SwitchCommander.Application.Features.UserFeatures.ReadUser.Records;

public record ReadUserRequest(Guid Id) : IRequest<ReadUserResponse>;