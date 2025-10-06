using Microsoft.AspNetCore.Mvc;
using MediatR;
namespace Tp_Grupal.Controllers
{
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentController(IMediator mediator)
        {
            this.mediator = mediator;
        }



        [HttpGet]// Prueba para ver que ande todo.
       
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
