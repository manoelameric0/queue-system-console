using System;
using QueueManagementSystem.Console.Policies;

namespace QueueManagementSystem.Tests;

public class CallOrderPolicyTest
{
    [Fact]
    public void Policy_Should_Return_Normal_When_Counter_Is_Less_Than_Three()
    {
        // Arrange
        var regra = new CallOrderPolicy();

        // Act
        var resultado = regra.Proximo(0, 2);

        // Assert
        Assert.Equal("Normal", resultado);
    }
}
