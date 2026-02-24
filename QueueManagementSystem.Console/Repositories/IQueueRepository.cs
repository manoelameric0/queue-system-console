using System;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Console.Repositories;


    


public interface IQueueRepository
{
    void Add(Client client);
    void Remove();
    IEnumerable<Client> GetAll();
}
