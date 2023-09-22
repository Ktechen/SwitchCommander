using Microsoft.AspNetCore.Mvc;

namespace SwitchCommander.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{

}