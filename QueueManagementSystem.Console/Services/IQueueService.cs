using System;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Console.Services;

public interface IQueueService
{
    string Add(string Name);
    Client CallNext();
    Stack<Client> UndoLastCall();
    List<Client> GetQueues();
    Stack<Client> GetClients();

}
