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

        if (contador < 3) contador++;

        //adiciona os cliente Comuns
        if (type == ClientType.Comum)
        {
            _normalQueue.Enqueue(new Client (name:name, clientType:(int)type,enQueueTime:DateTime.Now));
        }

        //adiciona  os clientes Prioridade
        if (type == ClientType.Prioridade)
        {
            _PreferentialQueue.Enqueue(new Client (name:name, clientType:(int)type,enQueueTime:DateTime.Now));
        }

        //adiciona todos os clientes no repository
        _repository.Add(new Client
        (name:name, clientType:(int)type, enQueueTime:DateTime.Now));

    }

    public Client? CallNext()
    {
        Client? client = null;
        if (contador == 3) _PreferentialQueue.TryDequeue(out client);
        

        if (contador < 3)
        {
            var filaFinal = new Queue<Client> (_normalQueue.Concat(_PreferentialQueue).OrderByDescending(c => c.EnQueueTime));

            var clientOrderned = filaFinal.Dequeue();

            


        }

        if (_normalQueue == null && _PreferentialQueue == null)
        {
            throw new ArgumentException("Nenhum Cliente em Espera");
        }
        
        if(_repository.GetAll().FirstOrDefault() != null)
        {
            _history.Push(_repository.GetAll().FirstOrDefault()!);
        }

        

        return client;

    }

    public void UndoLastCall()
    {
        if (_history == null)
        {
            throw new ArgumentException("Nenhum Cliente Atendido Até o Momento!!!");
        }
    }


}
