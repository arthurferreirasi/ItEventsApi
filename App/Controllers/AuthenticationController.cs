using DotNetEnv;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{

    [HttpPost]
    public IActionResult Auth(string keyword)
    {
        if (keyword == Env.GetString("Auth_Key"))
        {
            var token = TokenService.GenerateToken(keyword);
            return Ok(token);
        }

        return BadRequest("keyword invalid!");
    }
}