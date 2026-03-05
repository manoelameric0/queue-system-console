using QueueManagementSystem.Console;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Services;
namespace QueueManagementSystem.Tests;

public class QueueServiceTests
{
    [Fact]
    public void AddClient_Should_Add_Client_To_Queue()
    {   //Arrange → preparar cenário
        var service = new QueueService();

        //Act → executar ação
        service.Add("Manoel", ClientType.Comum);

        //Assert → verificar resultado
        var Clients = service.GetClients();
        Assert.Single(Clients);
    }

    [Fact]
    public void AddClient_Should_Throw_Exception_When_Name_Is_Empty()
    {
        // Given
        var service = new QueueService();


        // When
        var exeception = Assert.Throws<ArgumentException>(() => service.Add("", ClientType.Comum));

        // Then
        Assert.Equal("Nome inválido", exeception.Message);
    }

    [Fact]
    public void AddClient_Should_Throw_Exception_When_Name_Is_Short()
    {
        // Given
        var service = new QueueService();

        // When
        var exception = Assert.Throws<ArgumentException>(() => service.Add("Ma", ClientType.Prioridade));
        

        // Then
        Assert.Equal("Nome inválido: mínimo 3 caracteres e sem números.", exception.Message);
    }

    [Fact]
    public void CallNext_Should_Call_Priority_After_Three_Normal()
    {
        //Arrange → preparar cenário
        var service = new QueueService();

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Comum);
        service.Add("Madry", ClientType.Comum);
        service.Add("Manoelle", ClientType.Prioridade);

        //Act → executar ação
        service.CallNext();
        service.CallNext();
        service.CallNext();
        service.CallNext();

        //Assert → verificar resultado
        var historico = service.GetHistory();

        Assert.Equal("Manoelle", historico.First().Name);
    }

    // [Fact]
    // public void UndoCallLast_Should_Reinsert_Last_Called_Client()
    // {
    //     // Arrange → preparar cenário
    //     var service = new QueueService();

    //     service.Add("Manoel", ClientType.Comum);
    //     service.Add("Andryelle", ClientType.Comum);
    //     service.Add("Madry", ClientType.Comum);
    //     service.Add("Manoelle", ClientType.Prioridade);

    //     // Act → executar ação
    //     service.CallNext(); // Manoel sai da fila
    //     service.CallNext(); // Andryelle sai da fila
    //     service.CallNext(); // Manoelle sai da fila
    //     service.UndoLastCall();

    //     // Assert → verificar resultado
    //     var Clients = service.GetClients();
    //     Assert.Equal("Madry", Clients.First().Name);
    // }
}
