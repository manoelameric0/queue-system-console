using System;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Interfaces;

public interface IQueueService
{
    Task Add(string name, ClientType type);
    Task CallNext();
    Task<Client>? UndoLastCall();
    Task<IEnumerable<Client>> GetClients();
    Task<IEnumerable<Client>> GetHistory();
    Task<QueueState> GetQueueState();
    Task<bool> HasClients();
    Task<bool> HasHistory();
    Task<Client> GetPreview();

}
