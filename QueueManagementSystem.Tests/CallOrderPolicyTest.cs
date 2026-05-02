using System;
using System.Net.Http.Headers;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Policies;
using QueueManagementSystem.Core.Services;
using QueueManagementSystem.Core.Interfaces;
using QueueManagementSystem.Core.Models;

namespace QueueManagementSystem.Tests;

public class CallOrderPolicyTest
{
    private readonly CallOrderPolicy _policy = new();

    [Fact]
    public void CallOrderType_WithLast3AllCommon_AndPriorityExists_ReturnsPriority()
    {
        // Arrange
        var history = new List<Client>();
        history.Add(new Client("Manoel", ClientType.Normal));
        history.Add(new Client("Andryelle", ClientType.Normal));
        history.Add(new Client("Madry", ClientType.Normal));

        // Act
        var resultado = _policy.CallOrderType(history, true);

        // Assert
        Assert.Equal(ClientType.Preferential, resultado);
    }

    [Fact]
    public void CallOrderType_WithLast3AllCommon_AndNoPriority_ReturnsCommon()
    {
        // Arrange
        var history = new List<Client>();
        history.Add(new Client("Manoel", ClientType.Normal));
        history.Add(new Client("Andryelle", ClientType.Normal));
        history.Add(new Client("Madry", ClientType.Normal));

        // Act
        var resultado = _policy.CallOrderType(history, false);

        // Assert
        Assert.Equal(ClientType.Normal, resultado);
    }

    [Fact]
    public void
    CallOrderType_WithFewerThan3InHistory_AndPriorityExists_ReturnsCommon()
    {
        // Arrange
        var history = new List<Client>();
        history.Add(new Client("Manoel", ClientType.Normal));
        history.Add(new Client("Andryelle", ClientType.Preferential));

        // Act
        var resultado = _policy.CallOrderType(history, true);

        // Assert
        Assert.Equal(ClientType.Normal, resultado);
    }

    [Fact]
    public void
    CallOrderType_WithLast3NotAllCommon_AndPriorityExists_ReturnsCommon()
    {
        // Arrange
        var history = new List<Client>();
        history.Add(new Client("Manoel", ClientType.Normal));
        history.Add(new Client("Manoelle", ClientType.Normal));
        history.Add(new Client("Andryelle", ClientType.Preferential));

        // Act
        var resultado = _policy.CallOrderType(history, true);

        // Assert
        Assert.Equal(ClientType.Normal, resultado);
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
        Assert.Equal(ClientType.Normal, resultado);
    }

}
