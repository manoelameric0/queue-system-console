using System;
using Microsoft.VisualBasic;
using QueueManagementSystem.Console.Enums;
using QueueManagementSystem.Console.Models;
using QueueManagementSystem.Console.Services;

namespace QueueManagementSystem.Console.UI;

public class Menu
{

    public void Executar(IQueueService service)
    {

        while (true)
        {
            //MENU RUNNING
            System.Console.Clear();
            System.Console.WriteLine("========================================");
            ShowInfo("    SISTEMA DE GERENCIAMENTO DE FILA    ");
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
                        ShowError($"\nError: {ex.Message}");
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
                        ShowError($"\nError: {ex.Message}");
                    }
                    break;

                case MenuOption.DisplayAll:
                    DisplayHistoryClients(service);
                    break;

                case MenuOption.Exit:
                    return;
                default:
                    ShowError("\nOpção Inválida \n");
                    break;
            }

        }


    }
    //validações
    static int ReadInt()
    {
        int option;
        while (!int.TryParse(System.Console.ReadLine(), out option) || option < 0)
        {
            ShowError("\nCaráctere inválido");
            System.Console.Write("\nDigite sua escolha: ");
        }

        return option;
    }

    static string ReadString()
    {
        string input = System.Console.ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(input) || input.Any(char.IsDigit) || int.TryParse(input, out int a))
        {
            ShowError("\nValor inválido!!!");
            System.Console.Write("\nDigite o nome do cliente: ");
            input = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    //Ações de Console
    static void AddClient(IQueueService service)
    {
        System.Console.Clear();
        System.Console.WriteLine("========================================");
        ShowInfo("         ADICIONAR CLIENTE");
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
        ShowSuccess("Cliente Adicionado a Fila");

    }

    static void CallNext(IQueueService service)
    {
        if (!service.GetClients().Any())
        {
            \n("\nNenhum Cliente em Espera!");
            System.Console.WriteLine("\n----------------------------------------");
        System.Console.Write("Pressione [Qualquer Tecla] para voltar");
        System.Console.ReadKey();
        }
        service.CallNext();
    }

    static void UndoLastCall(IQueueService service)
    {
        if (!service.GetClients().Any())
        {
            ShowError("\nNenhum Cliente Atendido até o momento!");
            System.Console.WriteLine("\n----------------------------------------");
            System.Console.Write("Pressione [Qualquer Tecla] para voltar");
            System.Console.ReadKey();
        }
        service.UndoLastCall();
    }

    static void DisplayHistoryClients(IQueueService service)
    {
        System.Console.Clear();
        System.Console.WriteLine("========================================");
        ShowInfo("         FILAS E HISTÓRICO");
        System.Console.WriteLine("========================================");
        System.Console.WriteLine("");

        var clientsComum = service.GetClients().Where(c => c.ClientType == ClientType.Comum);
        var clientsPriority = service.GetClients().Where(c => c.ClientType == ClientType.Prioridade);
        var history = service.GetHistory();

        if (!clientsComum.Any() && !clientsPriority.Any() && !history.Any()) ShowInfo("Nenhum Cliente Atendido até o Momento");


        if (clientsComum.Any())
        {
            ShowInfo("Fila Comum:");
            foreach (var client in clientsComum)
            {
                System.Console.WriteLine($"- {client.Name} | Horario de Chegada: {client.EnQueueTime:HH:mm}");
            }
        }

        if (clientsPriority.Any())
        {
            ShowInfo("Fila Preferencial:");
            foreach (var client in clientsPriority)
            {
                System.Console.WriteLine($"- {client.Name} | Horario de Chegada: {client.EnQueueTime:HH:mm}");
            }
        }


        if (history.Any())
        {
            ShowInfo("Histórico de atendimentos:");
            foreach (var client in history)
            {
                System.Console.WriteLine($"- {client.Name} ({client.ClientType}) | Hora de Chegada: {client.EnQueueTime:HH:mm}");
            }
        }

        System.Console.WriteLine("----------------------------------------");
        System.Console.Write("Pressione [Qualquer Tecla] para voltar");
        System.Console.ReadKey();
    }

    static void ShowError(string message)
    {
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(message);
        System.Console.ResetColor();
    }
    static void ShowSuccess(string message)
    {
        System.Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine(message);
        System.Console.ResetColor();
    }
    static void ShowInfo(string message)
    {
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine(message);
        System.Console.ResetColor();
    }
}
