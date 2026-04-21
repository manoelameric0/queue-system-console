using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueManagementSystem.API.DTOs;
using QueueManagementSystem.Core.Interfaces;

namespace QueueManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost("client")]
        public IActionResult AddClient(CreateClientRequest request)
        {
            _queueService.Add(request.Name, request.Type);

            return Ok("Client adicionado com sucesso");
        }

        [HttpPost("call-next")]
        public IActionResult CallNext()
        {
            if (!_queueService.HasClients())
            {
                return NoContent();
            }
            
            _queueService.CallNext();
            return Ok();

            
        }

    }

}

