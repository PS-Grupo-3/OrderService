using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        [HttpGet("Test")]
        public IActionResult GetTest()
        {
            return Ok(new { Message = "Event endpoint is working!" });
        }




    }
}
