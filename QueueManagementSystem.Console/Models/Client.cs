using System;
using QueueManagementSystem.Console.Enums;

namespace QueueManagementSystem.Console.Models;

public class Client
{
    public string Name {get;}
    public Guid ID {get;}
    public ClientType ClientType {get;}
    public DateTime? EnQueueTime;

    public Client(string name,ClientType clientType, DateTime? enQueueTime)
    {
        if (string.IsNullOrWhiteSpace(name) && name.Length < 3)
        {
            throw new ArgumentException("Nome Inválido!!!");
        }

        if (name.Any(char.IsDigit))
        {
            throw new ArgumentException("O nome não pode conter Numeros!!!");
        }

        if (enQueueTime == null)
        {
            throw new ArgumentException("Hora de Chegada Inválida!!!");
        }

        if (clientType != ClientType.Prioridade && clientType != ClientType.Comum)
        {
            throw new ArgumentException("Tipo Inválido");
        }

        Name = name;
        ID = Guid.NewGuid();
        ClientType = clientType;
        EnQueueTime = enQueueTime;
    }
}
