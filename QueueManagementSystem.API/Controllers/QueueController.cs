using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> AddClient(CreateClientRequest request)
        {
            await _queueService.Add(request.Name, request.Type);

            return Created("",request);
        }

        [HttpPost("call-next")]
        public async Task<IActionResult> CallNext()
        {
            // caso a fila esteja vazia 204
            if (!await _queueService.HasClients())
            {
                return NoContent();
            }

            await _queueService.CallNext();
            return Ok();


        }

        [HttpGet("get-queue-state")]
        public async Task<IActionResult> GetQueueState()
        {
            var queueState = await _queueService.GetQueueState();

            if (!queueState.HasClients()) return NoContent();

            var response = new QueueStateResponse
            {
                NormalQueue = queueState.Normal.Select(ClientMapper.MapClient),
                PreferentialQueue = queueState.Preferential.Select(ClientMapper.MapClient),
                History = queueState.History.Select(ClientMapper.MapClient)
            };

            return Ok(response);
        }

        [HttpPost("undo")]
        public async Task<IActionResult> UndoLastCall()
        {
            
            var hasOrNo = await _queueService.HasHistory();
            if (!hasOrNo) return NoContent();

            var client = await _queueService.UndoLastCall();
            var response = new ClientResponse
            {
                Name = client.Name,
                Type = client.Type.ToString(),
                QueuedAt = client.QueuedAt,
                CalledAt = client.CalledAt,
            };

            return Ok(response);
        }

    }

}

