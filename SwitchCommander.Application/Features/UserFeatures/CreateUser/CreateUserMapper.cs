﻿using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.UserFeatures.CreateUser;

[Mapper]
public partial class CreateUserMapper
{
    public partial User FromRequest(CreateUserRequest dto);
    public partial CreateUserResponse ToResponse(User dto);
}