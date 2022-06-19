using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Threading.Tasks;
using Webproject.BLL.Interfaces;

namespace WebApplication1.Controller
{
    [ApiController]
    [Route("[controller]")]

public class ClientController : ControllerBase
{
    private readonly IRabbitMqService _rabbitMq;



    public ClientController(IRabbitMqService rabbitMq)
    {
        _rabbitMq = rabbitMq;


    }
      [HttpPost("sendClient")]
        public async Task<IActionResult> SendClient([FromBody] Client c)
        {
            await _rabbitMq.PublishClientAsync(c);

            return Ok();
        }
    }
}
