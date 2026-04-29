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

        public async Task<IEnumerable<Client>> GetQueue() => await _dbContext.Clients.AsNoTracking().Where(c => c.CalledAt == null).ToListAsync();

        public async Task<IEnumerable<Client>> GetHistory() => await _dbContext.Clients.AsNoTracking().Where(c => c.CalledAt != null).ToListAsync();

        public async Task Remove(Client client)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(string name) => await _dbContext.Clients.AsNoTracking().AnyAsync(c => c.Name == name);

        public async Task<bool> HasHistory() => await _dbContext.Clients.AsNoTracking().AnyAsync(c=> c.CalledAt != null);
        public async Task<bool> HasClients() => await _dbContext.Clients.AsNoTracking().AnyAsync(c => c.CalledAt == null);

    }
}
