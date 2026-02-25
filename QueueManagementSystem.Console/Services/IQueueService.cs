using System;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Console.Services;

public interface IQueueService
{
    void Add(string name, ClientType type);
    Client? CallNext();
    void UndoLastCall();
    IEnumerable<Client>? GetClients();
    IEnumerable<Client>? GetHistory();

}
