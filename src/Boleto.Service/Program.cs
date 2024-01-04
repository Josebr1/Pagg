using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapPost("/boleto", async (Pagg.Core.Entities.Boleto boleto, IBus bus) =>
{
    var queue = configuration.GetSection("MassTransit")["QueueName"];

    var endpoint = await bus.GetSendEndpoint(new Uri($"queue:{queue}"));
    await endpoint.Send(boleto);

    return Results.Ok();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();