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
    Stack<Client> _history = new();
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

        var clientType = policy.CallOrderType(_history, _PreferentialQueue.Any());

        if (clientType == ClientType.Prioridade)
        {
            if (_PreferentialQueue.TryDequeue(out var atendidoPriority))
            {
                _history.Push(atendidoPriority);
                contador = 0;
                return;
            }

        }

        var filaFinal = new Queue<Client>(_normalQueue.Concat(_PreferentialQueue).OrderBy(c => c.EnQueueTime));

        if (filaFinal.Any())
        {
            if(filaFinal.TryDequeue(out var client))
            _history.Push(client);
        }
        
    }

    public Client? UndoLastCall()
    {
        //FINALIZAR O RETORNO DO UNDO PARA APARECER NO MENU!!!
        Client? client = default;

        if (_history.Any())
        {
            client = _history.Pop();

            if (client.ClientType == ClientType.Comum)
            {
                _normalQueue = new Queue<Client>(new[] { client }.Concat(_normalQueue));
                if (contador != 0) contador -= 1;
                return client;
            }
            else
            {
                _PreferentialQueue = new Queue<Client>(new[] { client }.Concat(_PreferentialQueue));
                contador = 0;
                return client;
            }

        }
        return client;
    }

    public IEnumerable<Client> GetClients()
    {

        var clients = new Queue<Client>(_normalQueue.Concat(_PreferentialQueue).OrderBy(c => c.EnQueueTime));

        return clients ?? Enumerable.Empty<Client>();
    }

    public IEnumerable<Client> GetHistory() => _history ?? Enumerable.Empty<Client>();


    void AddHistory(Client client)
    {
        if (client.ClientType == ClientType.Comum)
        {
            if (_normalQueue.TryDequeue(out var atendidoNormal))
            {

                _history.Push(atendidoNormal);
                contador++;
            }
        }

        if (client.ClientType == ClientType.Prioridade)
        {
            if (_PreferentialQueue.TryDequeue(out var atendidoPriority))
            {
                _history.Push(atendidoPriority);
                contador = 0;
            }
        }
    }

    public int GetContador()
    {
        return contador;
    }

}
