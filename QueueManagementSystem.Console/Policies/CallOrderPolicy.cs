using System;

namespace QueueManagementSystem.Console.Policies;

public class CallOrderPolicy
{
   public string Proximo(int quantidadeAtendido, int prioridade)
    {
        if (quantidadeAtendido == 3 && prioridade > 0)
        {
            return string.Format("Prioridade");
        }
        return string.Format("Normal");
    }
}
