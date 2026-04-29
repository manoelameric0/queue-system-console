using System;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Interfaces;

public interface IQueueService
{
    Task Add(string name, ClientType type);
    //void CallNext();
    //Client? UndoLastCall();
    Task<IEnumerable<Client>> GetClients();
    Task<IEnumerable<Client>> GetHistory();
    Task<QueueState> GetQueueState();
    //bool HasClients();
    //bool HasHistory();
    //Client? GetPreview();

}
