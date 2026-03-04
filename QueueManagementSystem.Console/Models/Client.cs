using System;
using QueueManagementSystem.Console.Enums;

namespace QueueManagementSystem.Console.Models;

public class Client
{
    public string Name {get;}
    public Guid ID {get;}
    public ClientType ClientType {get;}
    public DateTime EnQueueTime;

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

        if (clientType != ClientType.Prioridade && clientType != ClientType.Comum)
        {
            throw new ArgumentException("Tipo de cliente inválido.");
        }

        Name = name;
        ID = Guid.NewGuid();
        ClientType = clientType;
        EnQueueTime = DateTime.Now;
    }
}
