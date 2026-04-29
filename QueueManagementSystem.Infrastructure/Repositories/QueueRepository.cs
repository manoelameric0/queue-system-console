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

        public async Task<List<Client>> GetAll()
        {
            return _dbContext.Clients.AsNoTracking().ToList();
        }


    }
}
