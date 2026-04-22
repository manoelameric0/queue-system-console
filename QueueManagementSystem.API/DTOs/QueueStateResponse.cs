using System;
using System.Runtime.InteropServices;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.API.DTOs;

public class QueueStateResponse
{
    public IEnumerable<ClientResponse> Comun {get; set;}
    public IEnumerable<ClientResponse> Prioridade {get; set;}
    public IEnumerable<ClientResponse> History {get; set;}

    public QueueStateResponse(IEnumerable<ClientResponse> comun, IEnumerable<ClientResponse> prioridade, IEnumerable<ClientResponse> history)
    {
        Comun = comun;
        Prioridade = prioridade;
        History = history;
    }
    public QueueStateResponse()
    {
        
    }

    public bool HasClients()
    {
        if (Comun.Any() && Prioridade.Any()) return true;

        return false;
    }

    
}
