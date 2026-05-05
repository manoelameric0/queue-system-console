using Microsoft.EntityFrameworkCore;
using QueueManagementSystem.Core.Interfaces;
using QueueManagementSystem.Core.Models;
using System;

namespace QueueManagementSystem.Infrastructure.Repositories;

public class InMemoryQueueRepository : IQueueRepository
{
    private readonly List<Client> _clientQueue = new();

    public async Task Add(Client client)
    {
        _clientQueue.Add(client);
    }

    public async Task Remove(Client client) => _clientQueue.Remove(client);

    public async Task<IEnumerable<Client>> GetQueue()
     => _clientQueue.Where(c => c.CalledAt == null).OrderBy(c => c.QueuedAt).ToList() ?? Enumerable.Empty<Client>();
    public async Task<IEnumerable<Client>> GetHistory()
    => _clientQueue.Where(c => c.CalledAt != null).OrderBy(c => c.CalledAt).ToList() ?? Enumerable.Empty<Client>();

    public async Task<bool> Exists(string name) => _clientQueue.Any(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

    public async Task<bool> HasHistory() =>  _clientQueue.Any(c => c.CalledAt != null);
    public async Task<bool> HasClients() => _clientQueue.Any(c => c.CalledAt == null);

    public async Task SaveChanges()
    {

    }



}
