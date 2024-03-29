﻿using MediatR;
using MongoDB.Driver;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features;

public class UserMongoRepository : BaseMongoRepository<User>, IUserMongoRepository
{
    public UserMongoRepository(MongoDbSshContext sshContext, IMediator mediator) : base(sshContext.UserCollection, mediator)
    {
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await Collection.Find(x => x.Email == email).FirstAsync(cancellationToken);
    }
}