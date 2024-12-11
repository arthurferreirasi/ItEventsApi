using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly ISymplaService _eventService;
    public EventsController(ISymplaService svc) {
        _eventService = svc;
     }

    [HttpGet]
    [Authorize]
    public IActionResult Get() { 
        try {
            var result = new List<Events>();
            result = this._eventService.GetItEvents(result);
            return Ok(result);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}