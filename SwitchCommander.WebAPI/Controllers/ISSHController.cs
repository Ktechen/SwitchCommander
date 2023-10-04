using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.CreateSSHCommand;
using SwitchCommander.Application.Features.SSH.DeleteSSHCommand;
using SwitchCommander.Application.Features.SSH.ReadSSHCommand;
using SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

namespace SwitchCommander.WebAPI.Controllers;

public interface ISSHController
{
    public Task<ActionResult<CreateSSHServerResponse>> CreateServer(CreateSSHServerRequest request, CancellationToken cancellationToken);
    public Task<ActionResult<ReadSSHServerResponse>> ReadServer(ReadSSHServerRequest request, CancellationToken cancellationToken);
    public Task<ActionResult<DeleteSSHServerResponse>> DeleteServer(DeleteSSHServerResponse request, CancellationToken cancellationToken);
    public Task<ActionResult<UpdateSSHServerResponse>> UpdateServer(CreateSSHServerRequest request, CancellationToken cancellationToken);
    
    public Task<ActionResult<CreateSSHCommandResponse>> CreateServer(CreateSSHCommandRequest request, CancellationToken cancellationToken);
    public Task<ActionResult<ReadSSHCommandResponse>> ReadServer(ReadSSHCommandRequest request, CancellationToken cancellationToken);
    public Task<ActionResult<DeleteSSHCommandResponse>> DeleteServer(DeleteSSHCommandResponse request, CancellationToken cancellationToken);
    public Task<ActionResult<UpdateSSHCommandResponse>> UpdateServer(UpdateSSHCommandRequest request, CancellationToken cancellationToken);
}
