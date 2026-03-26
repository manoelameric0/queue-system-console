using System;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Policies;
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
        _repository.Add(new Client(name: "Manoel", clientType: Console.Enums.ClientType.Comum));

        // Assert
        var clients = Assert.Single(_repository.GetAll());

        Assert.Equal("Manoel", clients.Name);
    }

    [Fact]
    public void Add_Should_Throw_Exception_When_Client_Is_Duplicated()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
         var _policy = new CallOrderPolicy();
        var service = new QueueService(_repository, _policy);

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
        var _repository = new InMemoryQueueRepository();
         var _policy = new CallOrderPolicy();
        var service = new QueueService(_repository, _policy);

        service.Add("Manoel", ClientType.Comum);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add("Manoel", ClientType.Comum));

        // Assert
        Assert.Single(_repository.GetAll());
    }

    [Fact]
    public void Remove_Should_Remove_Client_From_Repository()
    {
        // Arrange
        var repository = new InMemoryQueueRepository();

        repository.Add(new Client("Manoel", ClientType.Comum));
        repository.Add(new Client("Andryelle", ClientType.Comum));


        // Act
        repository.Remove(repository.GetAll().First());

        // Assert
        Assert.Single(repository.GetAll());
    }

    [Fact]
    public void GetClients_Should_Return_All_Clients()
    {
        // Arrange
        var repository = new InMemoryQueueRepository();

        // Act
        repository.Add(new Client("Manoel", ClientType.Comum));
        repository.Add(new Client("Andryelle", ClientType.Comum));
        repository.Add(new Client("Carlos", ClientType.Comum));

        // Assert
        Assert.Equal(3, repository.GetAll().Count());
    }

    [Fact]
    public void GetClients_Should_Return_Empty_When_No_Clients_Exist()
    {
        // Arrange
        var repository = new InMemoryQueueRepository();

        // Act
        repository.Add(new Client("Manoel", ClientType.Comum));
        var client = repository.GetAll().First();
        repository.Remove(client);

        // Assert
        Assert.Empty(repository.GetAll());
    }

    [Fact]
    public void GetClients_Should_Return_Clients_In_Correct_Order()
    {
        // Arrange
        var repository = new InMemoryQueueRepository();

        // Act
        repository.Add(new Client("Manoel", ClientType.Comum));
        repository.Add(new Client("Andryelle", ClientType.Comum));
        repository.Add(new Client("Carlos", ClientType.Comum));
        repository.Add(new Client("Jullia", ClientType.Comum));

        // Assert
        Assert.Equal("Andryelle", repository.GetAll().ElementAt(1).Name);
    }

    [Fact]
    public void Exists_Should_Return_True_When_Client_Already_Exists()
    {
        // Arrange
        var repository = new InMemoryQueueRepository();

        repository.Add(new Client("Manoel", ClientType.Comum));

        // Act
        bool existe = repository.Exists("Manoel");

        // Assert
        Assert.True(existe);
    }

    [Fact]
    public void Exists_Should_Return_False_When_Client_Does_Not_Exist()
    {
        // Arrange
        var repository = new InMemoryQueueRepository();

        repository.Add(new Client("Manoel", ClientType.Comum));

        // Act
        bool existe = repository.Exists("Andryelle");

        // Assert
        Assert.False(existe);
    }

}
