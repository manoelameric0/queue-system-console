using System;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Console.Services;

public interface IQueueService
{
    void Add(string name, ClientType type);
    void CallNext();
    Client UndoLastCall();
    IEnumerable<Client> GetClients();
    IEnumerable<Client> GetHistory();

}
