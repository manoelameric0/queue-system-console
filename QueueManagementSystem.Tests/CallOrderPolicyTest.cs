using System;
using System.Net.Http.Headers;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Policies;
using QueueManagementSystem.Console.Services;

namespace QueueManagementSystem.Tests;

public class CallOrderPolicyTest
{
    [Fact]
    public void Policy_Should_Return_Normal_When_Counter_Is_Less_Than_Three()
    {
        // Arrange
        var regra = new CallOrderPolicy();
        var service = new QueueService();

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Comum);
        service.Add("Madry", ClientType.Comum);
        service.Add("Suh", ClientType.Prioridade);
        // Act
        var resultado = regra.CallOrderType(service.GetClients(), true);

        // Assert
        Assert.Equal(ClientType.Prioridade, resultado);
    }

    // [Fact]
    // public void Policy_Should_Return_Priority_When_Counter_Is_Three()
    // {
    //     // Arrange
    //     var regra = new CallOrderPolicy();
    //     var service = new QueueService();

    //     service.Add("Manoel", ClientType.Comum);
    //     service.Add("Andryelle", ClientType.Comum);
    //     service.Add("Madry", ClientType.Comum);
    //     service.Add("Manoelle", ClientType.Prioridade);
    //     // Act
    //     service.CallNext();
    //     service.CallNext();
    //     service.CallNext();
    //     service.CallNext();

    //     bool temPrioridade = service.GetClients().Any(c => c.ClientType == ClientType.Prioridade);
    //     var resultado = regra.CallOrderType(service.GetHistory(), temPrioridade);

    //     // Assert
    //     Assert.Equal(ClientType.Prioridade, resultado);
    // }

    [Fact]
    public void Policy_Should_Keep_Returning_Normal_When_No_Priority_Clients()
    {
        // Arrange
        var regra = new CallOrderPolicy();
        var service = new QueueService();

        service.Add("Manoel", ClientType.Comum);
        service.Add("Andryelle", ClientType.Comum);
        service.Add("Madry", ClientType.Comum);
        service.Add("Manoelle", ClientType.Comum);
        // Act
        var resultado = regra.CallOrderType(service.GetClients(), false);

        // Assert
        Assert.Equal(ClientType.Comum, resultado);
    }
}
