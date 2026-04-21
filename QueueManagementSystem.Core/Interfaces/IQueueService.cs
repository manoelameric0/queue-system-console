using System;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Interfaces;

public interface IQueueService
{
    void Add(string name, ClientType type);
    void CallNext();
    Client? UndoLastCall();
    IEnumerable<Client> GetClients();
    IEnumerable<Client> GetHistory();
    bool HasClients();
    bool HasHistory();
    Client? GetPreview();

}
