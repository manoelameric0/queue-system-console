using Microsoft.Extensions.FileSystemGlobbing;
using QueueManagementSystem.Core.Interfaces;
using QueueManagementSystem.Core.Policies;
using QueueManagementSystem.Core.Services;
using QueueManagementSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// service
builder.Services.AddScoped<IQueueService, QueueService>();

//Repository
builder.Services.AddSingleton<IQueueRepository, InMemoryQueueRepository>();

//Policy
builder.Services.AddSingleton<ICallOrderPolicy, CallOrderPolicy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
