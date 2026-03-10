using System;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Console.Repositories;

public class InMemoryQueueRepository:IQueueRepository
{
    private readonly List<Client> _clientQueue = new();

    public void Add(Client client)
    {
        _clientQueue.Add(client);
    }

    public void Remove()
    {
        _clientQueue.Remove(_clientQueue.FirstOrDefault()!);
    }

    public void RestoreClient(Client client) => Add(client);

    public IEnumerable<Client> GetAll()
     => _clientQueue.OrderBy(c => c.EnQueueTime);
    
}
