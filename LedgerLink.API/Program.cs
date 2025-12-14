using LedgerLink.Application;
using LedgerLink.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// 2. Build the app

var app = builder.Build();

// 3. Configure Http requests

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map the controllers so the API knows where to route requests
app.MapControllers();

app.Run();