using System;
using System.Runtime.InteropServices;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.API.DTOs;

public class QueueStateResponse
{
    public IEnumerable<ClientResponse> NormalQueue {get; set;}
    public IEnumerable<ClientResponse> PreferentialQueue {get; set;}
    public IEnumerable<ClientResponse> History {get; set;}

    public QueueStateResponse(IEnumerable<ClientResponse> comun, IEnumerable<ClientResponse> prioridade, IEnumerable<ClientResponse> history)
    {
        NormalQueue = comun;
        PreferentialQueue = prioridade;
        History = history;
    }
    public QueueStateResponse()
    {
        
    }

    public bool HasClients()
    {
        if (NormalQueue.Any() && PreferentialQueue.Any()) return true;

        return false;
    }

    
}
