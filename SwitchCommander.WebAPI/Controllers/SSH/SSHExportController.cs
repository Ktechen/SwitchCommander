using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Common.Enums;
using SwitchCommander.Application.Common.Exceptions;
using SwitchCommander.Application.Features.SSH.Read.Export;

namespace SwitchCommander.WebAPI.Controllers.SSH;

public class SSHExportController : BaseController
{
    private readonly ILogger<SSHExportController> _logger;
    
    public SSHExportController(IMediator mediator, ILogger<SSHExportController> logger) : base(mediator)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadSSHExportJsonRequest>> GetExport(SshImportExportType type, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new ReadSSHExportJsonRequest(type), cancellationToken);
            return Ok(result);
        }
        catch (ExportTypeException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
}