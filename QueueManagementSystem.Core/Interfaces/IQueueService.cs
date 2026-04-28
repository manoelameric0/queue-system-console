using System;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Interfaces;

public interface IQueueService
{
    Task Add(string name, ClientType type);
    //void CallNext();
    //Client? UndoLastCall();
    //IEnumerable<Client> GetClients();
    //IEnumerable<Client> GetHistory();
    //QueueState GetQueueState();
    //bool HasClients();
    //bool HasHistory();
    //Client? GetPreview();

}
