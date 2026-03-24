using QueueManagementSystem.Console;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Services;
namespace QueueManagementSystem.Tests;

public class QueueServiceTests
{
    [Fact]
    public void Add_Should_Add_Normal_Client_When_Valid()
    {   //Arrange → preparar cenário
        var service = new QueueService();

        //Act → executar ação
        service.Add("Manoel", ClientType.Comum);

        //Assert → verificar resultado
        var Clients = service.GetClients();
        Assert.Equal(ClientType.Comum, Clients.First().ClientType);
    }

    [Fact]
    public void Add_Should_Add_Priority_Client_When_Valid()
    {   //Arrange → preparar cenário
        var service = new QueueService();

        //Act → executar ação
        service.Add("Manoel", ClientType.Prioridade);

        //Assert → verificar resultado
        var Clients = service.GetClients();
        Assert.Equal(ClientType.Prioridade, Clients.First().ClientType);
    }

    [Fact]
    public void Add_Should_Throw_Exception_When_Client_Is_Duplicated()
    {
        // Given
        var service = new QueueService();

        service.Add("Manoel", ClientType.Prioridade);
        // When
        var exeception = Assert.Throws<ArgumentException>(() => service.Add("Manoel", ClientType.Comum));

        // Then
        Assert.Equal("Cliente já está na Fila", exeception.Message);
    }

    [Fact]
    public void Add_Should_Not_Modify_Queue_When_Duplicate_Exception_Is_Thrown()
    {
        // Given
        var service = new QueueService();

        service.Add("Manoel", ClientType.Prioridade);
        // When
        var exeception = Assert.Throws<ArgumentException>(() => service.Add("Manoel", ClientType.Comum));

        // Then
        Assert.Single(service.GetClients());
    }

    [Fact]
    public void CallNext_Should_Call_First_Client_When_Queue_Has_Clients()
    {
        //Arrange → preparar cenário
        var service = new QueueService();

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Comum);
        service.Add("Madry", ClientType.Comum);
        service.Add("Manoelle", ClientType.Prioridade);

        //Act → executar ação
        service.CallNext();

        //Assert → verificar resultado
        var historico = service.GetHistory();

        Assert.Equal("Manoel", historico.First().Name);
    }

    [Fact]
    public void CallNext_Should_Follow_Three_Normal_To_One_Priority_Rule()
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

        //Assert → verificar resultado


        Assert.Equal("Madry", service.GetHistory().First().Name);
    }

    [Fact]
    public void CallNext_Should_Call_Clients_In_Sequence_Based_On_Arrival_Order()
    {
        //Arrange → preparar cenário
        var service = new QueueService();

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Prioridade);
        service.Add("Madry", ClientType.Comum);
        service.Add("Fagna", ClientType.Comum);
        service.Add("vitoria", ClientType.Comum);
        service.Add("Mylenna", ClientType.Comum);
        service.Add("Manoelle", ClientType.Prioridade);

        //Act → executar ação
        service.CallNext();
        service.CallNext();
        service.CallNext();
        service.CallNext();
        service.CallNext();
        service.CallNext();

        //Assert → verificar resultado


        Assert.Equal("Manoelle", service.GetHistory().First().Name);
    }

    [Fact]
    public void CallNext_Should_Call_Priority_After_Three_Normal_Calls()
    {
        //Arrange → preparar cenário
        var service = new QueueService();

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Comum);
        service.Add("Madry", ClientType.Comum);
        service.Add("Carlos", ClientType.Comum);
        service.Add("Manoelle", ClientType.Prioridade);

        //Act → executar ação
        service.CallNext();
        service.CallNext();
        service.CallNext();
        service.CallNext();

        //Assert → verificar resultado
        Assert.Equal("Manoelle", service.GetHistory().First().Name);
    }


}
