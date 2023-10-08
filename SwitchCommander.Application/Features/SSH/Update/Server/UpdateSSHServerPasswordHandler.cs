using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SwitchCommander.Application.Common.Services;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Update.Server;

public sealed record UpdateSSHServerPasswordRequest
    (string Id, string Password) : IRequest<UpdateSSHServerPasswordResponse>;

public sealed record UpdateSSHServerPasswordResponse(bool Result);

public class UpdateSSHServerPasswordHandler : IRequestHandler<UpdateSSHServerPasswordRequest, UpdateSSHServerPasswordResponse>
{
    private readonly ILogger<UpdateSSHServerPasswordHandler> _logger;
    private readonly ISshServerMongoRepository _mongoRepository;
    private readonly IPasswordService _passwordService;

    public UpdateSSHServerPasswordHandler(
        ILogger<UpdateSSHServerPasswordHandler> logger, 
        ISshServerMongoRepository mongoRepository,
        UpdateSSHServerMapper mapper, IPasswordService passwordService)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
        _passwordService = passwordService;
    }

    public async Task<UpdateSSHServerPasswordResponse> Handle(UpdateSSHServerPasswordRequest request, CancellationToken cancellationToken)
    {
        // Define the filter to identify the document to update
        var filter = Builders<SSHServer>.Filter.Eq(x => x.Id, new Guid(request.Id));

        var hash = await _passwordService.HashPassword(request.Password);
        
        // Define the update operation
        var update = Builders<SSHServer>.Update
            .Set(x => x.Password, hash)
            .Set(x => x.DateUpdated, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        var result = await _mongoRepository.UpdateAsync(filter, update, cancellationToken);
        
        _logger.LogInformation("Hostname: {0} is updated: {1}", request.Id, result);
        return new UpdateSSHServerPasswordResponse(result);
    }
}