using System;
using QueueManagementSystem.Core.Enums;

namespace QueueManagementSystem.API.DTOs;

public class ClientResponse
{
    public string Name {get; set;}
    public ClientType ClientType {get;set;}
    public DateTime EnQueueTime {get; set;}
    public DateTime? CallTime {get; set;} 

    public ClientResponse()
    {
        
    }

}
