using System;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Console.Policies;

public interface ICallOrderPolicy
{
    public ClientType CallOrderType(IEnumerable<Client> clients, bool havePriority);
}
