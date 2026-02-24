using System;
using Microsoft.VisualBasic;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Repositories;

namespace QueueManagementSystem.Console.Services;

public class QueueService : IQueueService
{
    int contador = 0;
    Stack<Client> _history = new();

    private readonly IQueueRepository _repository;
    public QueueService(IQueueRepository repository)
    {
        _repository = repository;
    }


    public void Add(string name, ClientType type)
    {
        if (_repository.GetAll().Equals(c))
        {
            
        }        

    }

    Client CallNext()
    {

    }
}
