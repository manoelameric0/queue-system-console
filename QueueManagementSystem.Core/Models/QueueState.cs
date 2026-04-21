using System;

namespace QueueManagementSystem.Core.Models;

public class QueueState
{
    public IEnumerable<Client> Comun{get; private set;}
    public IEnumerable<Client> Prioridade{get; private set;}
    public IEnumerable<Client> History{get; private set;}

    public QueueState(IEnumerable<Client> comun, IEnumerable<Client> prioridade, IEnumerable<Client> history)
    {
        Comun = comun;
        Prioridade = prioridade;
        History = history;

    }

    public bool HasClients()
    {
        if (Comun.Any() && Prioridade.Any()) return true;

        return false;
    }
    
}
