using Alert.Service;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;

builder.Services.AddMassTransit(x =>
{
    var queue = configuration.GetSection("MassTransit")["QueueName"];
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
    });
});

var host = builder.Build();
host.Run();
