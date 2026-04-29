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


    public async Task Add(string name, ClientType type)
    {
        if (await _repository.Exists(name))
        {
            throw new ArgumentException("Cliente já está na Fila");
        }

        //adiciona todos os clientes no repository
        await _repository.Add(new Client
        (Name: name, Type: type));

    }

    //public void CallNext()
    //{
    //    var clients = GetClients();

    //    if (clients.Any())
    //    {
    //        var clientType = _policy.CallOrderType(_history, clients.Any(c => c.Type == ClientType.Preferential));

    //        if (clientType == ClientType.Preferential)
    //        {
    //            var client = clients.First(c => c.Type == ClientType.Preferential);

    //            client.AddCallTime();
    //            AddAtHistory(client);
    //            _repository.Remove(client);

    //            if (_history.Count() > 20) _history.RemoveAt(0);
    //        }
    //        else
    //        {
    //            var client = clients.First();

    //            client.AddCallTime();
    //            AddAtHistory(client);
    //            _repository.Remove(client);
    //            if (_history.Count() > 20) _history.RemoveAt(0);
    //        }

    //    }

    //}

    //public Client? UndoLastCall()
    //{
    //    //FINALIZAR O RETORNO DO UNDO PARA APARECER NO MENU!!!
    //    Client? client = default;

    //    if (_history.Any())
    //    {
    //        client = GetHistory().Last();

    //        _repository.Add(client);
    //        _history.Remove(client);

    //    }
    //    return client;
    //}

    public async Task<QueueState> GetQueueState()
    {
        var clients = await GetClients();
        var normalClients = clients.Where(c => c.Type == ClientType.Normal);
        var preferentialClients = clients.Where(c => c.Type == ClientType.Preferential);
        var history = await GetHistory();

        return new QueueState(comun: normalClients, prioridade: preferentialClients, history: history);
    }

    public async Task<IEnumerable<Client>> GetClients() => await _repository.GetQueue() ?? Enumerable.Empty<Client>();

    public async Task<IEnumerable<Client>> GetHistory() => await _repository.GetHistory() ?? Enumerable.Empty<Client>();

    public void AddAtHistory(Client client)
    {
        if (client.CalledAt == null)
        {
            throw new ArgumentException("Falha no Atendimento");
        }
        _history.Add(client);
    }

    public async Task<bool> HasClients() => await _repository.HasClients();
    public async Task<bool> HasHistory() => await _repository.HasHistory();
    //public Client? GetPreview()
    //{
    //    var clients = GetClients();
    //    var type = _policy.CallOrderType(_history, clients.Any(c => c.Type == ClientType.Preferential));

    //    return type == ClientType.Preferential ? clients.FirstOrDefault(c => c.Type == ClientType.Preferential) : clients.FirstOrDefault();

    //}

}
