using System;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.API.DTOs;

public class ClientMapper
{
    public static ClientResponse MapClient(Client c)
    {
        return new ClientResponse
        {
            Name = c.Name,
            Type = c.Type.ToString(),
            QueuedAt = c.QueuedAt,
            CalledAt = c.CalledAt

        };
            
        
    }
}
