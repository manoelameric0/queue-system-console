using System;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Console.Policies;

public class CallOrderPolicy : ICallOrderPolicy
{
   public ClientType CallOrderType(IEnumerable<Client> clients, bool havePriority)
    {
        var threeLasts = clients.TakeLast(3).Count(c => c.ClientType == ClientType.Comum);
        
        if (threeLasts == 3 && havePriority)
        {
            return ClientType.Prioridade;

        }
        return ClientType.Comum;
    }
}
