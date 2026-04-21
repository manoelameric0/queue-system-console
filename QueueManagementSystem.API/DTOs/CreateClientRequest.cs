using System;
using QueueManagementSystem.Core.Enums;

namespace QueueManagementSystem.API.DTOs;

public class CreateClientRequest
{
    public string Name {get; set;}
    public ClientType Type {get; set;}
}
