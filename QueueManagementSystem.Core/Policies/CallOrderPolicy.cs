using System;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;
using QueueManagementSystem.Core.Interfaces;

namespace QueueManagementSystem.Core.Policies;

public class CallOrderPolicy : ICallOrderPolicy
{
   public ClientType CallOrderType(IEnumerable<Client> clients, bool havePriority)
    {
        var threeLasts = clients.TakeLast(3).Count(c => c.Type == ClientType.Normal);
        
        if (threeLasts == 3 && havePriority)
        {
            return ClientType.Preferential;

        }
        return ClientType.Normal;
    }
}
