using System;
using QueueManagementSystem.Console.Services;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Repositories;

namespace QueueManagementSystem.Tests;

public class ClientTests
{
    [Fact]
    public void Client_Should_Create_When_Name_Is_Valid()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository);

        // Act
        service.Add("Manoel", ClientType.Comum);

        // Assert
        Assert.Equal("Manoel", service.GetClients().First().Name);
    }

    [Fact]
    public void Client_Should_Exception_When_Name_Is_Empty()
    {
        // Arrange
       var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository);

        ;
        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add(string.Empty, ClientType.Prioridade));

        // Assert
        Assert.Equal("Nome inválido", exception.Message);
    }

    [Fact]
    public void Client_Should_Exception_When_Name_Is_White_Space()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository);

        ;
        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add(" ", ClientType.Prioridade));

        // Assert
        Assert.Equal("Nome inválido", exception.Message);
    }

    [Fact]
    public void Client_Should_Exception_When_Name_Is_Shorter_Than_Three_Characters()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository);

        ;
        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add("Ma", ClientType.Comum));

        // Assert
        Assert.Equal("Nome inválido: mínimo 3 caracteres e sem números.", exception.Message);
    }

    [Fact]
    public void Client_Should_Exception_When_Name_Contains_Number()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository);

        ;
        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add("Manoel123", ClientType.Comum));

        // Assert
        Assert.Equal("O nome não pode conter números.", exception.Message);
    }

    [Fact]
    public void Client_Should_Set_Type_Correctly_When_Valid()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository);

        // Act
        service.Add("Manoel", ClientType.Prioridade);

        // Assert
        Assert.Equal(ClientType.Prioridade, service.GetClients().First().ClientType);
    }

    [Fact]
    public void Client_Should_Throw_Exception_When_Type_Is_Invalid()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var service = new QueueService(_repository);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add("Manoel", (ClientType)3));

        // Assert
        Assert.Equal("Tipo de cliente inválido.", exception.Message);
    }
}
