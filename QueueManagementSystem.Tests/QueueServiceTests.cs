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
}
