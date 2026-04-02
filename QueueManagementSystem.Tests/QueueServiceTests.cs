using QueueManagementSystem.Console;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Repositories;
using QueueManagementSystem.Console.Services;
namespace QueueManagementSystem.Tests;
using Moq;
using QueueManagementSystem.Console.Models;

public class QueueServiceTests
{
    private readonly Mock<IQueueRepository> _repository = new();
    private readonly Mock<ICallOrderPolicy> _policy = new();
    private readonly QueueService _service;
    public QueueServiceTests()
    {
        _service = new QueueService(_repository.Object, _policy.Object);
    }

    [Fact]
    public void Add_WithDuplicateName_ThrowsArgumentException()
    {
        // Arrange
        _repository
        .Setup(r => r.Exists(It.IsAny<string>()))
        .Returns(true);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => _service.Add("Manoel", ClientType.Comum));

        // Assert
        Assert.Equal("Cliente já está na Fila", exception.Message);
    }

    [Fact]
    public void CallNext_WhenClientsExist_ReturnsNextInOrder()
    {
        // Arrange
        _repository
        .SetupSequence(r => r.GetAll())
        .Returns(new List<Client>(){new Client("Manoel", ClientType.Comum)})
        .Returns(new List<Client>(){new Client("Carlos", ClientType.Comum)})
        .Returns(new List<Client>(){new Client("Jullia", ClientType.Comum)});

        _repository
        .Setup(r => r.Remove(It.IsAny<Client>()));

        _policy
        .Setup(p => p.CallOrderType(_service.GetHistory(), false))
        .Returns(ClientType.Comum);

        // Act
        _service.CallNext();
        _service.CallNext();
        _service.CallNext();

        var history = _service.GetHistory();

        // Assert
        Assert.NotEmpty(history);
        Assert.Equal("Manoel", history.First()!.Name);
        Assert.Equal("Jullia", history.Last()!.Name);
        _repository.Verify(r => r.GetAll(), Times.Exactly(3));
    }

    [Fact]
    public void UndoLastCall_WhenHistoryHasClient_ReturnsClientAndRestoresQueue()
    {
        // Arrange
        var client = new Client("manoel", ClientType.Comum);

        client.AddCallTime();
        _service.AddAtHistory(client);

        // Act
        var getHistory = _service.UndoLastCall();

        // Assert
        Assert.Empty(_service.GetHistory());
        Assert.Equal("manoel", getHistory!.Name);
        _repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Once);
    }

    [Fact]
    public void UndoLastCall_WhenHistoryIsEmpty_ReturnsNull()
    {
        // Arrange

        // Act
        var getHistory = _service.UndoLastCall();

        // Assert
        Assert.Null(getHistory);
        _repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Never);
    }

}
