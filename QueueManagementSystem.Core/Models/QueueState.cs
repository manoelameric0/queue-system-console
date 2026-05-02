using System;

namespace QueueManagementSystem.Core.Models;

public class QueueState
{
    public IEnumerable<Client> Normal{get; private set;}
    public IEnumerable<Client> Preferential{get; private set;}
    public IEnumerable<Client> History{get; private set;}

    public QueueState(IEnumerable<Client> comun, IEnumerable<Client> prioridade, IEnumerable<Client> history)
    {
        Normal = comun;
        Preferential = prioridade;
        History = history;

    }

    public bool HasClients()
    {
        if (Normal.Any() || Preferential.Any() || History.Any()) return true;

        return false;
    }
    
}
