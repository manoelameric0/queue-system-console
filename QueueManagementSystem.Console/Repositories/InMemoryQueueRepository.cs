using System;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Console.Repositories;

public class InMemoryQueueRepository:IQueueRepository
{
    private readonly Queue<Client> _clientQueue = new();

    public void Add(Client client)
    {
        _clientQueue.Enqueue(client);
    }

    public void Remove()
    {
        _clientQueue.Dequeue();
    }

    public IEnumerable<Client> GetAll()
    {
        return _clientQueue;
    }
}
