using System;

namespace QueueManagementSystem.API.DTOs;

public class QueueStateRequest
{
    public IEnumerable<CreateClientRequest> Comun {get; private set;}
    public IEnumerable<CreateClientRequest> Prioridade {get; private set;}
    public IEnumerable<CreateClientRequest> History {get; private set;}

    public QueueStateRequest(IEnumerable<CreateClientRequest> comun, IEnumerable<CreateClientRequest> prioridade, IEnumerable<CreateClientRequest> history)
    {
        Comun = comun;
        Prioridade = prioridade;
        History = history;
    }
}
