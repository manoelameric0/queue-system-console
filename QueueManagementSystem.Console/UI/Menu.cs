using System;
using QueueManagementSystem.Console.Enums;
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
            var input = ReadInt();

            MenuOption menu = (MenuOption)input;
            switch (menu)
            {
                case MenuOption.Add:
                    try
                    {
                        AddClient(service);
                    }
                    catch (ArgumentException ex)
                    {
                        System.Console.WriteLine($"\nError: {ex.Message}");
                    }
                    break;

                case MenuOption.CallNext:
                    try
                    {
                        CallNext(service);
                    }
                    catch (ArgumentException ex)
                    {
                        System.Console.WriteLine($"\nError: {ex.Message}");
                    }
                    break;

                case MenuOption.UndoLastCall:
                    try
                    {
                        UndoLastCall(service);
                    }
                    catch (ArgumentException ex)
                    {
                        System.Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case MenuOption.Exit:
                    return;
                default:
                    System.Console.WriteLine("\nOpção Inválida!\n");
                    break;
            }

        }


    }
    //validações
    static int ReadInt()
    {
        int option;
        while (!int.TryParse(System.Console.ReadLine(), out option))
        {
            System.Console.WriteLine("Caráctere inválido");
        }

        return option;
    }

    static string ReadString()
    {
        string input = System.Console.ReadLine()!;
        while (string.IsNullOrWhiteSpace(input) || input.Any(char.IsDigit) || int.TryParse(input, out int a))
        {
            System.Console.WriteLine("Valor inválido!!!");
            System.Console.Write("Digite o nome do cliente: ");
            input = System.Console.ReadLine()!;

        }

        return input;
    }

    //Ações de Console
    static void AddClient(IQueueService service)
    {
        System.Console.WriteLine("========================================");
        System.Console.WriteLine("         ADICIONAR CLIENTE");
        System.Console.WriteLine("========================================");
        System.Console.WriteLine("");
        System.Console.Write("Digite o nome do cliente: ");
        string nome = ReadString();
        System.Console.WriteLine("\nEscolha o tipo de Cliente: ");
        System.Console.WriteLine("1 - Normal");
        System.Console.WriteLine("2 - Preferencial");
        System.Console.Write("Digite sua escolha: ");
        int priority = ReadInt();

        service.Add(nome, (ClientType)priority);

    }

    static void CallNext(IQueueService service) => service.CallNext();

    static void UndoLastCall(IQueueService service) => service.UndoLastCall();





    static void DisplayHistoryClients(IQueueService service)
    {
        System.Console.WriteLine("========================================");
        System.Console.WriteLine("         FILAS E HISTÓRICO");
        System.Console.WriteLine("========================================");
        System.Console.WriteLine("");

        var clientsComum = service.GetClients()!.Where(c => c.ClientType == ClientType.Comum);
        var clientsPriority = service.GetClients()!.Where(c => c.ClientType == ClientType.Prioridade);

        if (clientsComum.Any())
        {
            System.Console.WriteLine("Fila Normal:");
            foreach (var client in clientsComum)
            {
                System.Console.WriteLine($"- {client.Name} | Horario de Chegada: {client.EnQueueTime:HH:mm}");
            }
        }

        if (clientsPriority.Any())
        {
            System.Console.WriteLine("Fila Preferencial:");
            foreach (var client in clientsPriority)
            {
                System.Console.WriteLine($"- {client.Name} | Horario de Chegada: {client.EnQueueTime:HH:mm}");
            }
        }

        var history = service.GetHistory();
        if (history!.Any())
        {
            System.Console.WriteLine("Histórico de atendimentos:");
            foreach (var client in history!)
            {
                System.Console.WriteLine($"- {client.Name} ({client.ClientType}) | Hora de Chegada: {client.EnQueueTime}");
            }
        }

        System.Console.WriteLine("----------------------------------------");
        System.Console.Write("Pressione [Enter] para voltar");
        System.Console.ReadKey();
    }
}
