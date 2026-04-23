using System;
using System.Dynamic;
using Microsoft.VisualBasic;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;
using QueueManagementSystem.Core.Interfaces;

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
            var clientType = _policy.CallOrderType(_history, clients.Any(c => c.Type == ClientType.Preferential));

            if (clientType == ClientType.Preferential)
            {
                var client = clients.First(c => c.Type == ClientType.Preferential);

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

    public QueueState GetQueueState()
    {
        var clients = GetClients();
        var clientsComum = clients.Where(c => c.Type == ClientType.Normal);
        var clientsPriority = clients.Where(c => c.Type == ClientType.Preferential);
        var history = GetHistory();

        return new QueueState(comun: clientsComum, prioridade: clientsPriority, history: history);
    }

    public IEnumerable<Client> GetClients()
    {
        var clients = _repository.GetAll().OrderBy(c => c.QueuedAt).ToList();

        return clients ?? Enumerable.Empty<Client>();
    }

    public IEnumerable<Client> GetHistory() => _history ?? Enumerable.Empty<Client>();

    public void AddAtHistory(Client client)
    {
        if (client.CalledAt == null)
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
        var type = _policy.CallOrderType(_history, clients.Any(c => c.Type == ClientType.Preferential));

        return type == ClientType.Preferential ? clients.FirstOrDefault(c => c.Type == ClientType.Preferential) : clients.FirstOrDefault();

    }

}
