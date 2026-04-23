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

        [HttpPost("undo")]
        public IActionResult UndoLastCall()
        {
            var client = _queueService.UndoLastCall();

            if (client == null)  return NoContent();

            var response = new ClientResponse
            {
                Name = client.Name,
                ClientType = client.ClientType.ToString(),
                EnQueueTime = client.EnQueueTime,
                CallTime = client.CallTime,
            };

            return Ok(response);
        }

    }

}

