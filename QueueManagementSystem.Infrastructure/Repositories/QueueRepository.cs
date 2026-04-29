using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QueueManagementSystem.Core;
using QueueManagementSystem.Core.Interfaces;
using QueueManagementSystem.Core.Models;
using QueueManagementSystem.Infrastructure.Data;

namespace QueueManagementSystem.Infrastructure.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly QueueDbContext _dbContext;

        public QueueRepository(QueueDbContext context)
        {
            _dbContext = context;
        }

        public async Task Add(Client client)
        {
            await _dbContext.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Client>> GetQueue() => await _dbContext.Clients.AsNoTracking().Where(c => c.CalledAt == null).ToListAsync();

        public async Task<List<Client>> GetHistory() => await _dbContext.Clients.AsNoTracking().Where(c => c.CalledAt != null).ToListAsync();

        public async Task<bool> Exists(string name) => await _dbContext.Clients.AsNoTracking().AnyAsync(c => c.Name == name);

    }
}
