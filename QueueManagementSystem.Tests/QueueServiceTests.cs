using QueueManagementSystem.Console;
using QueueManagementSystem.Core.Enums;
using QueueManagementSystem.Core.Interfaces;
using QueueManagementSystem.Core.Services;
namespace QueueManagementSystem.Tests;
using Moq;
using QueueManagementSystem.Core.Models;
using Xunit.Sdk;

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
    public async Task Add_WithDuplicateName_ThrowsArgumentException()
    {
        // Arrange
        _repository
        .Setup(r => r.Exists(It.IsAny<string>()))
        .ReturnsAsync(true);

        // Act
        var exception = Assert.ThrowsAsync<ArgumentException>(() => _service.Add("Manoel", ClientType.Normal));

        // Assert
        Assert.Equal("Cliente já está na Fila", exception.Result.Message);
    }

    [Fact]
    public async Task CallNext_WhenClientsExist_ReturnsNextInOrder()
    {
        var c1 = new Client("Manoel", ClientType.Normal);
        var c2 = new Client("Carlos", ClientType.Normal);
        var c3 = new Client("Jullia", ClientType.Normal);
        // Arrange
        _repository
        .SetupSequence(r => r.GetQueue())
        .ReturnsAsync(new List<Client>(){c1, c2, c3})
        .ReturnsAsync(new List<Client>(){c2, c3})
        .ReturnsAsync(new List<Client>(){c3});

        _repository
            .Setup(r => r.GetHistory())
            .ReturnsAsync(new List<Client>() { c1, c2, c3 });

        _repository
        .Setup(r => r.Remove(It.IsAny<Client>()));

        _policy
        .Setup(p => p.CallOrderType(_service.GetHistory().Result.ToList(), false))
        .Returns(ClientType.Normal);

        // Act
        await _service.CallNext();
        await _service.CallNext();
        await _service.CallNext();

        var history = await _service.GetHistory();

        // Assert
        Assert.NotEmpty(history);
        Assert.Equal("Manoel", history.First()!.Name);
        Assert.Equal("Jullia", history.Last()!.Name);
        _repository.Verify(r => r.GetQueue(), Times.Exactly(3));
    }

    [Fact]
    public async Task UndoLastCall_WhenHistoryHasClient_ReturnsClientAndRestoresQueue()
    {
        // Arrange
        var client = new Client("manoel", ClientType.Normal);

        _repository
            .SetupSequence(r => r.GetHistory())
            .ReturnsAsync(Enumerable.Empty<Client>)
            .ReturnsAsync(new List<Client>() { client })
            .ReturnsAsync(Enumerable.Empty<Client>);
            

        client.AddCallTime();
        var history = await _service.GetHistory();
        await _service.AddAtHistory(client, history);

        // Act
        var getAtHistory = await _service.UndoLastCall();

        // Assert
        Assert.Empty(await _service.GetHistory());
        Assert.Equal("manoel", getAtHistory.Name);
        Assert.Null(getAtHistory.CalledAt);
    }

    [Fact]
    public async Task UndoLastCall_WhenHistoryIsEmpty_ReturnsNull()
    {
        // Arrange

        // Act
        var getHistory = await _service.UndoLastCall();

        // Assert
        Assert.Null(getHistory);
        _repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Never);
    }

}
