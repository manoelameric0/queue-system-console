using System;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Models;
using Microsoft.VisualBasic;

namespace QueueManagementSystem.Tests;

public class ClientTests
{
    [Fact]
    public void Constructor_WithValidNameAndType_CreatesClientSuccessfully()
    {
        // Arrange

        // Act
        var client = new Client("Manoel", ClientType.Normal);

        // Assert
        Assert.Equal("Manoel", client.Name);
        Assert.Equal(ClientType.Normal, client.Type);
        Assert.NotEqual(Guid.Empty, client.ID);
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Client("", ClientType.Normal));

        // Assert
        Assert.Equal("Nome inválido", exception.Message);
    }

    [Fact]
    public void Constructor_WithNameShorterThan3Chars_ThrowsArgumentException()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Client("ma", ClientType.Normal));

        // Assert
        Assert.Equal("Nome inválido: mínimo 3 caracteres e sem números.", exception.Message);
    }

    [Fact]
    public void Constructor_WithNameContainingDigits_ThrowsArgumentException()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Client("Manoel123", ClientType.Normal));

        // Assert
        Assert.Equal("O nome não pode conter números.", exception.Message);
    }

    [Fact]
    public void Constructor_WithInvalidClientType_ThrowsArgumentException()
    {
        // Arrange
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Client("Manoel", 0));

        // Assert
       Assert.Equal("Tipo de cliente inválido.", exception.Message);
    }

    [Fact]
    public void Constructor_WithValidName_SetsEnqueueTimeToNow()
    {
        // Arrange

        // Act
        var client = new Client("Manoel", ClientType.Normal);

        // Assert
        Assert.NotEqual(client.QueuedAt, DateTime.Now);
    }

    
}
