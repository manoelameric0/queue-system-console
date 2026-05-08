using System;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Core.Interfaces;

public interface ICallOrderPolicy
{
    public Task<ClientType> CallOrderType(IEnumerable<Client> clients, bool havePriority);
}
