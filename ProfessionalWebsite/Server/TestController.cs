using Microsoft.AspNetCore.Mvc;

namespace ProfessionalWebsite.Server;

[Route("api/{controller}")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("Test")]
    public async Task<ActionResult<string>> GetString()
    {
        //return new Ok("this string is guuuuuuuud");
        await Task.Delay(1000);
        return "this string is guuuuuuuud";
    }
}
