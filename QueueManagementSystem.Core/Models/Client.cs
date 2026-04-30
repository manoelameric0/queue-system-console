using System;
using System.Globalization;
using QueueManagementSystem.Core.Enums;

namespace QueueManagementSystem.Core.Models;

public class Client
{
    public string Name { get; private set; }
    public Guid ID {get; private set; }
    public ClientType Type {get; private set; }
    public DateTime QueuedAt {get; private set; }
    public DateTime? CalledAt {get; private set; } 

    public Client(string Name,ClientType Type)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("Nome inválido");
        }

        if (Name.Length < 3)
        {
            throw new ArgumentException("Nome inválido: mínimo 3 caracteres e sem números.");
        }

        if (Name.Any(char.IsDigit))
        {
            throw new ArgumentException("O nome não pode conter números.");
        }

        if (!Enum.IsDefined(typeof(ClientType), Type))
        {
            throw new ArgumentException("Tipo de cliente inválido.");
        }

        this.Name = Name;
        ID = Guid.NewGuid();
        this.Type = Type;
        QueuedAt = DateTime.Now;
    }

    public void AddCallTime()
    {
        CalledAt = DateTime.Now;
    }

    public void UndoCall()
    {
        CalledAt = null;
    }
}
