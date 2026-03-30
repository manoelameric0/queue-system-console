using System;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Repositories;
using QueueManagementSystem.Console.Services;

namespace QueueManagementSystem.Tests;

public class InMemoryQueueRepositoryTest
{   
    private readonly InMemoryQueueRepository _repository = new();

    [Fact]
    public void Add_WhenCalled_ClientAppearsInGetAll()
    {
        // Arrange
        var client = new Client("Manoel", ClientType.Prioridade);

        // Act
        _repository.Add(client);
        var clients = _repository.GetAll();

        // Assert
        Assert.Contains(clients, c => c.Name == "Manoel");
    }

    [Fact]
    public void Remove_WhenClientExists_ClientDisappearsFromGetAll()
    {
        // Arrange
        var client = new Client("Manoel", ClientType.Prioridade);
        _repository.Add(client);

        // Act
        _repository.Remove(client);
        var clients = _repository.GetAll();

        // Assert
        Assert.DoesNotContain(clients, c => c.Name == "Manoel");
    }

    [Fact]
    public void Exists_WhenClientAdded_ReturnsTrue()
    {
        // Arrange
        var client = new Client("Manoel", ClientType.Prioridade);
        _repository.Add(client);

        // Act
        var exist = _repository.Exists("Manoel");

        // Assert
        Assert.True(exist);
    }

    [Fact]
    public void Exists_WithDifferentCasing_ReturnsTrue()
    {
        // Arrange
        var client = new Client("MANOEL", ClientType.Prioridade);
        _repository.Add(client);

        // Act
        var exist = _repository.Exists("manoel");

        // Assert
        Assert.True(exist);
    }

}
