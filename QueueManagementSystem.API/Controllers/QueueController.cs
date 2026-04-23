using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueManagementSystem.API.DTOs;
using QueueManagementSystem.Core.Interfaces;
// eu estive aqui teste
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
            // caso a fila esteja vazia 204
            if (!_queueService.HasClients())
            {
                return NoContent();
            }

            _queueService.CallNext();
            return Ok();

            
        }

        [HttpGet("get-queue-state")]
        public IActionResult GetQueueState()
        {
            var queueState = _queueService.GetQueueState();

            if (!queueState.HasClients())
            {
                return NoContent();
            }

            var response = new QueueStateResponse
            {
                Comun = queueState.Comun.Select(ClientMapper.MapClient),
                Prioridade = queueState.Prioridade.Select(ClientMapper.MapClient),
                History = queueState.History.Select(ClientMapper.MapClient)
            };

            return Ok(response);
        }

    }

}

