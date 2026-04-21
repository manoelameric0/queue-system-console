using System;
using System.Dynamic;
using Microsoft.VisualBasic;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;
using QueueManagementSystem.Core.Policies;
using QueueManagementSystem.Core.Repositories;

namespace QueueManagementSystem.Core.Services;

public class QueueService : IQueueService
{
    private readonly List<Client> _history = new();

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

    public void CallNext()
    {
        var clients = GetClients();

        if (clients.Any())
        {
            var clientType = _policy.CallOrderType(_history, clients.Any(c => c.ClientType == ClientType.Prioridade));

            if (clientType == ClientType.Prioridade)
            {
                var client = clients.First(c => c.ClientType == ClientType.Prioridade);

                client.AddCallTime();
                AddAtHistory(client);
                _repository.Remove(client);

                if (_history.Count() > 20) _history.RemoveAt(0);
            }
            else
            {
                var client = clients.First();

                client.AddCallTime();
                AddAtHistory(client);
                _repository.Remove(client);
                if (_history.Count() > 20) _history.RemoveAt(0);
            }

        }

    }

    public Client? UndoLastCall()
    {
        //FINALIZAR O RETORNO DO UNDO PARA APARECER NO MENU!!!
        Client? client = default;

        if (_history.Any())
        {
            client = GetHistory().Last();

            _repository.Add(client);
            _history.Remove(client);

        }
        return client;
    }

    public IEnumerable<Client> GetClients()
    {
        var clients = _repository.GetAll().OrderBy(c => c.EnQueueTime).ToList();

        return clients ?? Enumerable.Empty<Client>();
    }

    public IEnumerable<Client> GetHistory() => _history ?? Enumerable.Empty<Client>();

    public void AddAtHistory(Client client)
    {
        if (client.CallTime == null)
        {
            throw new ArgumentException("Falha no Atendimento");
        }
        _history.Add(client);
    }

    public bool HasClients() => GetClients().Any();
    public bool HasHistory() => _history.Any();
    public Client? GetPreview()
    {
        var clients = GetClients();
        var type = _policy.CallOrderType(_history, clients.Any(c => c.ClientType == ClientType.Prioridade));

        return type == ClientType.Prioridade ? clients.FirstOrDefault(c => c.ClientType == ClientType.Prioridade) : clients.FirstOrDefault();

    }

}
