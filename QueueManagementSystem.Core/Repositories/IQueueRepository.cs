using System;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Repositories;


    


public interface IQueueRepository
{
    void Add(Client client);
    void Remove(Client client);
    IEnumerable<Client> GetAll();
    bool Exists(string name);
}
