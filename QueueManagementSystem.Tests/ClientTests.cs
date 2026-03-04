using System;
using QueueManagementSystem.Console.Services;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;

namespace QueueManagementSystem.Tests;

public class ClientTests
{
    [Fact]
    public void Client_Should_Create_When_Name_Is_Valid()
    {
        // Arrange
        var service = new QueueService();

        // Act
        service.Add("Manoel", ClientType.Comum);

        // Assert
        Assert.Equal("Manoel", service.GetClients().First().Name);
    }

    [Fact]
    public void Client_Should_Exception_When_Name_Is_Empty()
    {
        // Arrange
        var service = new QueueService();

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
        var service = new QueueService();

        ;
        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.Add(" ", ClientType.Prioridade));

        // Assert
        Assert.Equal("Nome inválido", exception.Message);
    }


}
