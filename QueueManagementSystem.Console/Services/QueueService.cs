using System;
using Microsoft.VisualBasic;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Repositories;

namespace QueueManagementSystem.Console.Services;

public class QueueService : IQueueService
{
    int contador = 0;
    List<Client> _history = new();
    Queue<Client> _normalQueue = new();
    Queue<Client> _PreferentialQueue = new();

    private readonly IQueueRepository _repository;
    public QueueService(IQueueRepository repository)
    {
        _repository = repository;
    }


    public void Add(string name, ClientType type)
    {
        if (_repository.Exists(name))
        {
            throw new ArgumentException("Cliente já está na Fila");
        }

        //adiciona os cliente Comuns
        if (type == ClientType.Comum)
        {
            _normalQueue.Enqueue(new Client(name: name, clientType: type));
        }

        //adiciona  os clientes Prioridade
        if (type == ClientType.Prioridade)
        {
            _PreferentialQueue.Enqueue(new Client(name: name, clientType: type));
        }

        //adiciona todos os clientes no repository
        _repository.Add(new Client
        (name: name, clientType: type));

    }

    public Client? CallNext()
    {
        //FINALIZAR O RETORNO DO UNDO PARA APARECER NO MENU!!!
        Client? client = default;

        if (_repository.GetAll().Any())
        {
            var policy = new CallOrderPolicy();
            var clients = _repository.GetAll();

            var clientType = policy.CallOrderType(_history, clients.Any(c => c.ClientType == ClientType.Prioridade));

            if (clientType == ClientType.Prioridade)
            {
                client = clients.First(c => c.ClientType == ClientType.Prioridade);

                _history.Add(client);
                _repository.Remove(client);

                if (_history.Count() > 20) _history.RemoveAt(0);
            }
            else
            {
                client = clients.First();

                _history.Add(client);
                _repository.Remove(client);
                if (_history.Count() > 20) _history.RemoveAt(0);
            }

        }
        return client;

    }

    public Client? UndoLastCall()
    {
        //FINALIZAR O RETORNO DO UNDO PARA APARECER NO MENU!!!
        Client? client = default;

        if (_history.Any())
        {
            client = GetHistory().First();

            _repository.Add(client);
            _history.Remove(client);

        }
        return client;
    }

    public IEnumerable<Client> GetClients()
    {
        var clients = _repository.GetAll().OrderBy(c => c.EnQueueTime);

        return clients ?? Enumerable.Empty<Client>();
    }

    public IEnumerable<Client> GetHistory() => _history.OrderByDescending(c => c.EnQueueTime) ?? Enumerable.Empty<Client>();

    public int GetContador()
    {
        return contador;
    }

}
