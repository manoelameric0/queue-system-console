using Microsoft.EntityFrameworkCore;
using QueueManagementSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueueManagementSystem.Infrastructure.Data
{
    public class QueueDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração
        public QueueDbContext(DbContextOptions<QueueDbContext> contextOptions) : base (contextOptions)
        {
        }

        // Definição das tabelas (DbSets)
        public DbSet<Client> Clients { get; set; }
    }
}
