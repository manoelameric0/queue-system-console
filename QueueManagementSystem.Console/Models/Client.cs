using System;

namespace QueueManagementSystem.Console.Models;

public class Client
{
    string Name {get;}
    int ID {get;} = 0;
    DateTime EnQueueTime;

    public Client(string name, int id, DateTime enQueueTime)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Nome Inválido!!!");
        }

        if (id <= 0)
        {
            throw new ArgumentException("ID Inválido!!!");
        }

        if (enQueueTime == null)
        {
            throw new ArgumentException("Hora de Chegada Inválida!!!");
        }

        Name = name;
        ID += id;
        EnQueueTime = enQueueTime;
    }
}
