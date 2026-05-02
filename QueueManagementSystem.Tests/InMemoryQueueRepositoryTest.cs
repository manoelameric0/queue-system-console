using System;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Policies;
using QueueManagementSystem.Core.Models;
using QueueManagementSystem.Infrastructure.Repositories;
using QueueManagementSystem.Core.Services;

namespace QueueManagementSystem.Tests;

public class InMemoryQueueRepositoryTest
{
    private readonly InMemoryQueueRepository _repository = new();

    [Fact]
    public async Task Add_WhenCalled_ClientAppearsInGetAll()
    {
        // Arrange
        var client = new Client("Manoel", ClientType.Preferential);

        // Act
        _repository.Add(client);
        var clients = await _repository.GetQueue();

        // Assert
        Assert.Contains(clients, c => c.Name == "Manoel");
    }

    [Fact]
    public async Task Remove_WhenClientExists_ClientDisappearsFromGetAll()
    {
        // Arrange
        var client = new Client("Manoel", ClientType.Preferential);
        await _repository.Add(client);

        // Act
        await _repository.Remove(client);
        var clients = await _repository.GetQueue();

        // Assert
        Assert.DoesNotContain(clients, c => c.Name == "Manoel");
    }

    [Fact]
    public async Task Exists_WhenClientAdded_ReturnsTrue()
    {
        // Arrange
        var client = new Client("Manoel", ClientType.Preferential);
        await _repository.Add(client);

        // Act
        var exist = await _repository.Exists("Manoel");

        // Assert
        Assert.True(exist);
    }

    [Fact]
    public async Task Exists_WithDifferentCasing_ReturnsTrue()
    {
        // Arrange
        var client = new Client("MANOEL", ClientType.Preferential);
        await _repository.Add(client);

        // Act
        var exist = await _repository.Exists("manoel");

        // Assert
        Assert.True(exist);
    }

}
