using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.EntityFrameworkCore;
using QueueManagementSystem.API.Middleware;
using QueueManagementSystem.Core.Interfaces;
using QueueManagementSystem.Core.Policies;
using QueueManagementSystem.Core.Services;
using QueueManagementSystem.Infrastructure.Data;
using QueueManagementSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// service
builder.Services.AddSingleton<IQueueService, QueueService>();

//Repository
builder.Services.AddSingleton<IQueueRepository, InMemoryQueueRepository>();
builder.Services.AddDbContext<QueueDbContext>(option => option.UseSqlite("Data Source=queue.db"));

//Policy
builder.Services.AddSingleton<ICallOrderPolicy, CallOrderPolicy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
