using System;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Repositories;
using QueueManagementSystem.Console.Services;

namespace QueueManagementSystem.Tests;

public class InMemoryQueueRepositoryTest
{
    [Fact]
    public void Add_Should_Add_Client_To_Repository()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();


        // Act
        _repository.Add(new Client(name:"Manoel",clientType: Console.Enums.ClientType.Comum));

        // Assert
        var clients = Assert.Single(_repository.GetAll());

        Assert.Equal("Manoel",clients.Name);
    }

    [Fact]
    public void Add_Should_Throw_Exception_When_Client_Is_Duplicated()
    {
        // Arrange
        var service = new QueueService();

        service.Add("Manoel", Console.Enums.ClientType.Comum);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add("Manoel", Console.Enums.ClientType.Comum));

        // Assert
        Assert.Equal("Cliente já está na Fila", exception.Message);
    }

    [Fact]
    public void Add_Should_Not_Add_Client_When_Duplicate_Exception_Is_Thrown()
    {
        // Arrange
        var repository = new InMemoryQueueRepository();
        var service = new QueueService(repository);

        service.Add("Manoel", ClientType.Comum);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add("Manoel",ClientType.Comum));

        // Assert
        Assert.Single(repository.GetAll());
    }

    [Fact]
    public void Remove_Should_Remove_Client_From_Repository()
    {
        // Arrange
        var repository = new InMemoryQueueRepository();

        repository.Add(new Client ("Manoel", ClientType.Comum));
        repository.Add(new Client ("Andryelle", ClientType.Comum));
        

        // Act
        repository.Remove(repository.GetAll().First());

        // Assert
        Assert.Single(repository.GetAll());
    }

}
