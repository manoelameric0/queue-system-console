using System;

namespace QueueManagementSystem.Console.Models;

public class Client
{
    string Name {get;}
    int ID {get;} = 0;
    bool Priority;
    DateTime EnQueueTime;

    public Client(string name, int id,bool priority, DateTime enQueueTime)
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

        if (priority == null)
        {
            throw new ArgumentException("Prioridade Inválida!!!");
        }

        Name = name;
        ID += id;
        Priority = priority;
        EnQueueTime = enQueueTime;
    }
}
