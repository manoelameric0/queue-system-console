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
    public QueueService()
    {
        _repository = new InMemoryQueueRepository();
    }


    public void Add(string name, ClientType type)
    {
        if (_repository.GetAll().Any(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException("Cliente já está na Fila");
        }

        if (name.Any(char.IsDigit))
        {
            throw new ArgumentException("Nome inválido não é permitido numero no nome do cliente   ");
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

    public void CallNext()
    {
        if (!_normalQueue.Any() && !_PreferentialQueue.Any())
        {
            throw new ArgumentException("Nenhum Cliente em Espera");
        }

        if (contador == 3)
        {
            if (_PreferentialQueue.Any())
            {
                if (_PreferentialQueue.TryDequeue(out var atendidoPriority))
                {
                    _history.Push(atendidoPriority);
                    contador = 0;
                }
            }
            else
            {
                if (_normalQueue.TryDequeue(out var atendidoNormal))
                {
                    _history.Push(atendidoNormal);
                    contador++;
                }



            }

        }

        var filaFinal = new Queue<Client>(_normalQueue.Concat(_PreferentialQueue).OrderBy(c => c.EnQueueTime));
        if (filaFinal.TryDequeue(out var atendido))
        {
            AddHistory(atendido);
        }



    }

    public void UndoLastCall()
    {
        if (!_history.Any())
        {
            throw new ArgumentException("Nenhum Cliente Atendido Até o Momento!!!");
        }

        var client = _history.Pop();

        if (client.ClientType == ClientType.Comum)
        {
            _normalQueue = new Queue<Client>(new[] { client }.Concat(_normalQueue));
            if (contador != 0) contador -= 1;
        }
        else
        {
            _PreferentialQueue = new Queue<Client>(new[] { client }.Concat(_PreferentialQueue));
            contador = 0;
        }


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

}
