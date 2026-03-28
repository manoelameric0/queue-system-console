using System;
using QueueManagementSystem.Console.Services;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Repositories;
using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Models;
using Microsoft.VisualBasic;

namespace QueueManagementSystem.Tests;

public class ClientTests
{
    [Fact]
    public void Constructor_WithValidNameAndType_CreatesClientSuccessfully()
    {
        // Arrange

        // Act
        var client = new Client("Manoel", ClientType.Comum);

        // Assert
        Assert.Equal("Manoel", client.Name);
        Assert.Equal(ClientType.Comum, client.ClientType);
        Assert.NotEqual(Guid.Empty, client.ID);
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Client("", ClientType.Comum));

        // Assert
        Assert.Equal("Nome inválido", exception.Message);
    }

    [Fact]
    public void Constructor_WithNameShorterThan3Chars_ThrowsArgumentException()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Client("ma", ClientType.Comum));

        // Assert
        Assert.Equal("Nome inválido: mínimo 3 caracteres e sem números.", exception.Message);
    }

    [Fact]
    public void Constructor_WithNameContainingDigits_ThrowsArgumentException()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Client("Manoel123", ClientType.Comum));

        // Assert
        Assert.Equal("O nome não pode conter números.", exception.Message);
    }

    [Fact]
    public void Client_Should_Exception_When_Name_Contains_Number()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var _policy = new CallOrderPolicy();
        var service = new QueueService(_repository, _policy);

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
        var _policy = new CallOrderPolicy();
        var service = new QueueService(_repository, _policy);

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
        var _policy = new CallOrderPolicy();
        var service = new QueueService(_repository, _policy);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add("Manoel", (ClientType)3));

        // Assert
        Assert.Equal("Tipo de cliente inválido.", exception.Message);
    }

    [Fact]
    public void Client_Should_Add_CallTime_When_CallNext_Is_Called()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var _policy = new CallOrderPolicy();
        var service = new QueueService(_repository, _policy);

        service.Add("Manoel", ClientType.Comum);
        // Act
        service.CallNext();
        var client = service.GetHistory().First();

        // Assert
        Assert.NotNull(client.CallTime);
    }

    [Fact]
    public void Client_Should_Add_CallTime_Correct_When_CallNext_Is_Called()
    {
        // Arrange
        var _repository = new InMemoryQueueRepository();
        var _policy = new CallOrderPolicy();
        var service = new QueueService(_repository, _policy);

        service.Add("Manoel", ClientType.Comum);
        // Act
        var older = DateTime.Now;
        service.CallNext();
        var client = service.GetHistory().First();
        var after = DateTime.Now;

        // Assert
        Assert.True(client.CallTime >= older && client.CallTime <= after);
    }
}
