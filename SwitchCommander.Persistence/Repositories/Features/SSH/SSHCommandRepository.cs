﻿using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features.SSH;


public class SSHCommandRepository : BaseRepository<SSHCommand>, ISSHCommandRepository
{
    public SSHCommandRepository(MongoDbContext context, IMediator mediator) : base(context.SSHCommandCollection,
        mediator)
    {
    }
}