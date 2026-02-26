using System;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Services;

namespace QueueManagementSystem.Console.UI;

public class Menu
{
    public void Executar()
    {
        IQueueService service = new QueueService();
        while (true)
        {
            //MENU RUNNING
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("    SISTEMA DE GERENCIAMENTO DE FILA    ");
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("");
            System.Console.WriteLine($"Client Atual: {(service.GetClients()?.FirstOrDefault() is Client c ? $"[ {c.Name} ({c.ClientType}) | Hora de Chegada: {c.EnQueueTime:HH:mm} ]" : "[ Nenhum cliente em atendimento ]")}");
            System.Console.WriteLine("");
            System.Console.WriteLine("----------------------------------------");
            System.Console.WriteLine("Selecione uma opção:");
            System.Console.WriteLine("1 - Adicionar cliente");
            System.Console.WriteLine("2 - Chamar próximo cliente");
            System.Console.WriteLine("3 - Desfazer último atendimento");
            System.Console.WriteLine("4 - Visualizar filas e histórico");
            System.Console.WriteLine("0 - Sair");
            System.Console.WriteLine("----------------------------------------");
            System.Console.WriteLine("");
            System.Console.Write("Digite sua escolha: ");

            if (true)
            {

            }

        }
    }
}
