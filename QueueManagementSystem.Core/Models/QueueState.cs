using System;

namespace QueueManagementSystem.Core.Models;

public class QueueState
{
    IEnumerable<Client> Comun;
    IEnumerable<Client> Prioridade;
    IEnumerable<Client> History;

    public QueueState(IEnumerable<Client> comun, IEnumerable<Client> prioridade, IEnumerable<Client> history)
    {
        Comun = comun;
        Prioridade = prioridade;
        History = history;

    }
    
}
