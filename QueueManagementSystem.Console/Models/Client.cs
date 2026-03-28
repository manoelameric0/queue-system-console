using System;
using System.Globalization;
using QueueManagementSystem.Console.Enums;

namespace QueueManagementSystem.Console.Models;

public class Client
{
    public string Name {get;}
    public Guid ID {get;}
    public ClientType ClientType {get;}
    public DateTime EnQueueTime {get;}
    public DateTime? CallTime {get; private set;} 

    public Client(string name,ClientType clientType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Nome inválido");
        }

        if (name.Length < 3)
        {
            throw new ArgumentException("Nome inválido: mínimo 3 caracteres e sem números.");
        }

        if (name.Any(char.IsDigit))
        {
            throw new ArgumentException("O nome não pode conter números.");
        }

        if (!Enum.IsDefined(typeof(ClientType), clientType))
        {
            throw new ArgumentException("Tipo de cliente inválido.");
        }

        Name = name;
        ID = Guid.NewGuid();
        ClientType = clientType;
        EnQueueTime = DateTime.Now;
    }

    public void AddCallTime()
    {
        CallTime = DateTime.Now;
    }
}
