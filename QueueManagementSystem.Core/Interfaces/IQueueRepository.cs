using System;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Interfaces;


    


public interface IQueueRepository
{
    Task Add(Client client);
    Task Remove(Client client);
    Task<IEnumerable<Client>> GetQueue();
    Task<IEnumerable<Client>> GetHistory();
    Task<bool> Exists(string name);
    Task<bool> HasHistory();
    Task<bool> HasClients();
    Task SaveChanges();
}
