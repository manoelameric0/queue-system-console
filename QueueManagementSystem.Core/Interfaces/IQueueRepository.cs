using System;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Interfaces;


    


public interface IQueueRepository
{
    Task Add(Client client);
    //void Remove(Client client);
    //IEnumerable<Client> GetAll();
    //bool Exists(string name);
}
