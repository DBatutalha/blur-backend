using BlurApi.Data;
using BlurApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BlurApi.Controllers
{
  [ApiController]
  [Route("api/sample")]
  public class SampleController : ControllerBase
  {
    private readonly AppDbContext db;

    public SampleController(AppDbContext _db)
    {
      db = _db;
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
      return GenericResponse.JustData(new { success = true });
    }

    [HttpPost("sample-request")]
    public IActionResult RequestSampleRequest([FromForm] string message)
    {
      if (message == "Ping")
      {
        message = "Pong";
      }
      return GenericResponse.JustData(new { user_message = message });
    }
  }
}