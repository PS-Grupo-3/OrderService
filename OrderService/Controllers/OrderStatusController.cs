
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok(new { Message = "Event endpoint is working!" });
        }
    }
}
