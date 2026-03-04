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
        Client client = new Client("Manoel", ClientType.Comum, DateTime.Now);

        // Assert
        Assert.Equal("Manoel", client.Name);
    }

    [Fact]
    public void Client_Should_Exception_When_Name_Is_White_Space()
    {
        // Arrange
        var service = new QueueService();

        ;
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Client("", ClientType.Prioridade, DateTime.Now));

        // Assert
        Assert.Equal("Nome inválido", exception.Message);
    }


}
