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



        //adiciona os cliente Comuns
        if (type == ClientType.Comum)
        {
            _normalQueue.Enqueue(new Client(name: name, clientType: type, enQueueTime: DateTime.Now));
        }

        //adiciona  os clientes Prioridade
        if (type == ClientType.Prioridade)
        {
            _PreferentialQueue.Enqueue(new Client(name: name, clientType: type, enQueueTime: DateTime.Now));
        }

        //adiciona todos os clientes no repository
        _repository.Add(new Client
        (name: name, clientType: type, enQueueTime: DateTime.Now));

    }

    public Client? CallNext()
    {

        if (!_normalQueue.Any() && !_PreferentialQueue.Any())
        {
            throw new ArgumentException("Nenhum Cliente em Espera");
        }

        var client = RemoveQueues();
        return client;

    }

    public Client? UndoLastCall()
    {
        if (!_history.Any())
        {
            throw new ArgumentException("Nenhum Cliente Atendido Até o Momento!!!");
        }

        var client = _history.FirstOrDefault();

        return client!;

    }

    public IEnumerable<Client>? GetClients()
    {
        if (!_normalQueue.Any() && !_PreferentialQueue.Any())
        {
            throw new ArgumentException("Nenhum Cliente Atendido Até o Momento!!!");
        }

        var clients = new Queue<Client>(_normalQueue.Concat(_PreferentialQueue).OrderByDescending(c => c.EnQueueTime));

        return clients;
    }

    public IEnumerable<Client>? GetHistory()
    {
        if (!_history.Any())
        {
            throw new ArgumentException("Nenhum Cliente Atendido Até o Momento!!!");
        }

        return _history;
    } 

    Client? RemoveQueues()
    {

        Client? client = null;
        if (contador <= 3)
        {
            var filaFinal = new Queue<Client>(_normalQueue.Concat(_PreferentialQueue).OrderByDescending(c => c.EnQueueTime));

            filaFinal.TryDequeue(out client);

            if (client!.ClientType == ClientType.Comum)
            {
                _normalQueue.Dequeue();
                _history.Push(client);
                contador++;

                return client;
            }

            if (client!.ClientType == ClientType.Prioridade)
            {
                _PreferentialQueue.Dequeue();
                _history.Push(client!);
                contador = 0;

                return client;
            }
        }

        if (contador == 3)
        {

            _PreferentialQueue.TryDequeue(out client);
            _history.Push(client!);
            contador = 0;
        }

        return client;

    }


}
