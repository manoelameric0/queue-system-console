using System;
using System.Net.Http.Headers;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Services;
using QueueManagementSystem.Console.Repositories;

namespace QueueManagementSystem.Tests;

public class CallOrderPolicyTest
{
    [Fact]
    public void Policy_Should_Return_Normal_When_History_Has_Less_Than_Three_Clients()
    {
        // Arrange
        var _policy = new CallOrderPolicy();
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository, _policy);

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Comum);
        service.Add("Madry", ClientType.Comum);
        service.Add("Suh", ClientType.Prioridade);
        // Act
        service.CallNext();
        service.CallNext();

        var resultado = _policy.CallOrderType(service.GetHistory(), true);

        // Assert
        Assert.Equal(ClientType.Comum, resultado);
    }

    [Fact]
    public void Policy_Should_Return_Normal_When_Last_Three_Are_Not_All_Normal()
    {
        // Arrange
        var _policy = new CallOrderPolicy();
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository, _policy);

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Prioridade);
        service.Add("Madry", ClientType.Comum);
        service.Add("Manoelle", ClientType.Prioridade);
        // Act
        service.CallNext();
        service.CallNext();
        service.CallNext();

        var resultado = _policy.CallOrderType(service.GetHistory(), true);

        // Assert
        Assert.Equal(ClientType.Comum, resultado);
    }

    [Fact]
    public void Policy_Should_Return_Priority_When_Last_Three_Are_All_Normal()
    {
        // Arrange
        var _policy = new CallOrderPolicy();
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository, _policy);

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Comum);
        service.Add("Madry", ClientType.Comum);
        service.Add("Manoelle", ClientType.Prioridade);
        // Act
        service.CallNext();
        service.CallNext();
        service.CallNext();

        var resultado = _policy.CallOrderType(service.GetHistory(), service.HasPrioty());

        // Assert
        Assert.Equal(ClientType.Prioridade, resultado);
    }
}
