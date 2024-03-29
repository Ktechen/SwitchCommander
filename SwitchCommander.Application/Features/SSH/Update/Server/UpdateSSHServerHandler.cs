﻿using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SwitchCommander.Application.Common.Services;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.SSH.Update.Server;

public sealed record UpdateSSHServerRequest
    (Guid Id, string Hostname, string Username) : IRequest<UpdateSSHServerResponse>;

public sealed record UpdateSSHServerResponse(bool result);

public class UpdateSSHServerHandler : IRequestHandler<UpdateSSHServerRequest, UpdateSSHServerResponse>
{
    private readonly ILogger<UpdateSSHServerHandler> _logger;
    private readonly UpdateSSHServerMapper _mapper;
    private readonly ISshServerMongoRepository _mongoRepository;

    public UpdateSSHServerHandler(ILogger<UpdateSSHServerHandler> logger, ISshServerMongoRepository mongoRepository,
        UpdateSSHServerMapper mapper, IPasswordService passwordService)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }

    public async Task<UpdateSSHServerResponse> Handle(UpdateSSHServerRequest request,
        CancellationToken cancellationToken)
    {
        // Define the filter to identify the document to update
        var filter = Builders<SSHServer>.Filter.Eq(x => x.Id, request.Id);

        // Define the update operation
        var update = Builders<SSHServer>.Update
            .Set(x => x.Hostname, request.Hostname)
            .Set(x => x.Username, request.Username)
            .Set(x => x.DateUpdated, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        var result = await _mongoRepository.UpdateAsync(filter, update, cancellationToken);

        _logger.LogInformation("Hostname: {0} is updated: {1}", request.Hostname, result);
        return new UpdateSSHServerResponse(result);
    }
}