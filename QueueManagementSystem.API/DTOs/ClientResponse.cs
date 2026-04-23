using System;
using QueueManagementSystem.Core.Enums;

namespace QueueManagementSystem.API.DTOs;

public class ClientResponse
{
    public string Name {get; set;}
    public string Type {get;set;}
    public DateTime QueuedAt {get; set;}
    public DateTime? CalledAt {get; set;} 

    public ClientResponse()
    {
        
    }

}
