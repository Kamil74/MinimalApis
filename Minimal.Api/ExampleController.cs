using Microsoft.AspNetCore.Mvc;

namespace Minimal.Api;
[ApiController]

public class ExampleController : ControllerBase
{
    [HttpGet("Test")]
    public ActionResult Test()
    {
        return Ok();
    }
}