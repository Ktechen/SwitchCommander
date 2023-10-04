﻿using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Common.Services;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.CreateSSHCommand;

public sealed record CreateSSHServerRequest
    (string? Hostname, string? Username, string? Password) : IRequest<CreateSSHServerResponse>;

public sealed record CreateSSHServerResponse(string? Hostname, string? Username);

public class CreateSSHServerHandler : IRequestHandler<CreateSSHServerRequest, CreateSSHServerResponse>
{
    private readonly ILogger<CreateSSHServerHandler> _logger;
    private readonly ISSHServerRepository _repository;
    private readonly CreateSSHServerMapper _mapper;
    private readonly IPasswordService _passwordService;
    
    public CreateSSHServerHandler(ILogger<CreateSSHServerHandler> logger, ISSHServerRepository serverRepository, CreateSSHServerMapper mapper, IPasswordService passwordService)
    {
        _logger = logger;
        _repository = serverRepository;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<CreateSSHServerResponse> Handle(CreateSSHServerRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create a server {0} ", request.Hostname);
        var map = _mapper.FromRequest(request);
        map.Password = await _passwordService.HashPassword(map.Password);
        await _repository.AddAsync(map, cancellationToken);
        return _mapper.ToResponse(map);
    }
}