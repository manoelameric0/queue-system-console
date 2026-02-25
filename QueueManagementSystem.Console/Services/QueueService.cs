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
    Queue<Client> _normalQueue = new();
    Queue<Client> _PreferentialQueue = new();

    private readonly IQueueRepository _repository;
    public QueueService(IQueueRepository repository)
    {
        _repository = repository;
    }


    public void Add(string name, ClientType type)
    {
        if (_repository.GetAll().Any(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)))
        {
           throw new ArgumentException("Cliente já está na Fila");
        }

        if (name.Any(char.IsDigit))
        {
            throw new ArgumentException("Nome inválido não é permitido numero no nome do cliente!!!");
        }

        contador++;

        _repository.Add(new Client(name:name, clientType:(int)type,id:contador, enQueueTime:DateTime.Now) );

    }

    public Client? CallNext()
    {
        if(_repository.GetAll().FirstOrDefault() != null)
        {
            _history.Push(_repository.GetAll().FirstOrDefault()!);
        }

        _repository.Remove();

        
        Client? client = _repository.GetAll().FirstOrDefault();

        return client;

    }

    public void UndoLastCall()
    {
        
    }
}
