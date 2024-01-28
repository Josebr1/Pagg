using Boleto.Api;
using Boleto.Api.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("pagg"));

var configuration = builder.Configuration;

builder.Services.AddMassTransit(x =>
{
    var server = configuration.GetSection("MassTransit")["Server"];
    var user = configuration.GetSection("MassTransit")["User"];
    var password = configuration.GetSection("MassTransit")["Password"];

    x.UsingRabbitMq((context, config) =>
    {
        config.Host(server, "/", h =>
        {
            h.Username(user);
            h.Password(password);
        });

        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapPost("/boleto", async (Pagg.Core.Entities.Boleto boleto, IBus bus, ApiContext db) =>
{
    var queue = configuration.GetSection("MassTransit")["QueueName"];

    db.Boletos.Add(boleto);
    await db.SaveChangesAsync();

    // Envia boleto para a fila de registros
    await new BankSlipRegistrationProduction(bus)
    .Send(queue, boleto);

    return Results.Ok(boleto);
});

app.MapGet("/boleto/{id}", async ([FromRoute] int id, ApiContext db) =>
{
    var boleto = await db.Boletos.FirstOrDefaultAsync(b => b.Id == id);

    if (boleto == null) return Results.NotFound();

    return Results.Ok(boleto);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();