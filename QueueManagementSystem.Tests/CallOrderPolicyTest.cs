using System;
using System.Net.Http.Headers;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Services;
using QueueManagementSystem.Console.Repositories;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Tests;

public class CallOrderPolicyTest
{
    private readonly CallOrderPolicy _policy = new();

    [Fact]
    public void CallOrderType_WithLast3AllCommon_AndPriorityExists_ReturnsPriority()
    {
        // Arrange
        var history = new List<Client>();
        history.Add(new Client("Manoel", ClientType.Comum));
        history.Add(new Client("Andryelle", ClientType.Comum));
        history.Add(new Client("Madry", ClientType.Comum));

        // Act
        var resultado = _policy.CallOrderType(history, true);

        // Assert
        Assert.Equal(ClientType.Prioridade, resultado);
    }

    [Fact]
    public void CallOrderType_WithLast3AllCommon_AndNoPriority_ReturnsCommon()
    {
        // Arrange
        var history = new List<Client>();
        history.Add(new Client("Manoel", ClientType.Comum));
        history.Add(new Client("Andryelle", ClientType.Comum));
        history.Add(new Client("Madry", ClientType.Comum));

        // Act
        var resultado = _policy.CallOrderType(history, false);

        // Assert
        Assert.Equal(ClientType.Comum, resultado);
    }

    [Fact]
    public void
    CallOrderType_WithFewerThan3InHistory_AndPriorityExists_ReturnsCommon()
    {
        // Arrange
        var history = new List<Client>();
        history.Add(new Client("Manoel", ClientType.Comum));
        history.Add(new Client("Andryelle", ClientType.Prioridade));

        // Act
        var resultado = _policy.CallOrderType(history, true);

        // Assert
        Assert.Equal(ClientType.Comum, resultado);
    }

    [Fact]
    public void
    CallOrderType_WithLast3NotAllCommon_AndPriorityExists_ReturnsCommon()
    {
        // Arrange
        var history = new List<Client>();
        history.Add(new Client("Manoel", ClientType.Comum));
        history.Add(new Client("Manoelle", ClientType.Comum));
        history.Add(new Client("Andryelle", ClientType.Prioridade));

        // Act
        var resultado = _policy.CallOrderType(history, true);

        // Assert
        Assert.Equal(ClientType.Comum, resultado);
    }

    [Fact]
    public void
    CallOrderType_WithEmptyHistory_AndPriorityExists_ReturnsCommon()
    {
        // Arrange
        var history = new List<Client>() ?? Enumerable.Empty<Client>();

        // Act
        var resultado = _policy.CallOrderType(history, true);

        // Assert
        Assert.Equal(ClientType.Comum, resultado);
    }

}
