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
    public QueueService()
    {
        _repository = new InMemoryQueueRepository();
    }
    public QueueService(InMemoryQueueRepository repository)
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

    public void CallNext()
    {
        if (!_repository.GetAll().Any()) throw new ArgumentException("Nenhum Cliente Aguardando para ser atendido.");

        var policy = new CallOrderPolicy();
        var clients = _repository.GetAll();

        var clientType = policy.CallOrderType(_history, clients.Any(c => c.ClientType == ClientType.Prioridade));

        if (clientType == ClientType.Prioridade)
        {
            var atendido = clients.First(c => c.ClientType == ClientType.Prioridade);

            _history.Add(atendido);
            _repository.Remove(atendido);
        }else
        {
            var atendido = clients.First();

            _history.Add(atendido);
            _repository.Remove(atendido);
        }

        
    }

    public Client? UndoLastCall()
    {
        //FINALIZAR O RETORNO DO UNDO PARA APARECER NO MENU!!!
        Client? client = default;

        if (_history.Any())
        {
            client = _history.First();

            _repository.Add(client);
            _history.Remove(client);

        }
        return client;
    }

    public IEnumerable<Client> GetClients()
    {

        var clients = new Queue<Client>(_normalQueue.Concat(_PreferentialQueue).OrderBy(c => c.EnQueueTime));

        return clients ?? Enumerable.Empty<Client>();
    }

    public IEnumerable<Client> GetHistory() => _history.OrderByDescending(c => c.EnQueueTime) ?? Enumerable.Empty<Client>();

    public int GetContador()
    {
        return contador;
    }

}
