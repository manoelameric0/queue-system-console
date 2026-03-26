using System;
using Microsoft.VisualBasic;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Repositories;

namespace QueueManagementSystem.Console.Services;

public class QueueService : IQueueService
{
    List<Client> _history = new();

    private readonly IQueueRepository _repository;
    private readonly ICallOrderPolicy _policy;
    public QueueService(IQueueRepository repository, ICallOrderPolicy policy)
    {
        _repository = repository;
        _policy = policy;
    }


    public void Add(string name, ClientType type)
    {
        if (_repository.Exists(name))
        {
            throw new ArgumentException("Cliente já está na Fila");
        }

        //adiciona todos os clientes no repository
        _repository.Add(new Client
        (name: name, clientType: type));

    }

    public Client? CallNext()
    {
        //FINALIZAR O RETORNO DO UNDO PARA APARECER NO MENU!!!
        Client? client = default;
        var clients = _repository.GetAll();

        if (clients.Any())
        {
            var clientType = _policy.CallOrderType(_history, HasPrioty());

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

    public bool HasPrioty() => _repository.GetAll().Any(c => c.ClientType == ClientType.Prioridade);
    public bool HasClients() => _repository.GetAll().Any();
    public bool HasHistory() => _history.Any();
    
}
