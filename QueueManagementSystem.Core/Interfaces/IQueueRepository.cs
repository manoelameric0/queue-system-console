using System;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Interfaces;


    


public interface IQueueRepository
{
    Task Add(Client client);
    //void Remove(Client client);
    Task<List<Client>> GetQueue();
    Task<List<Client>> GetHistory();
    Task<bool> Exists(string name);
}
