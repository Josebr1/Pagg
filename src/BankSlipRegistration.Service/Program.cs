using BankSlipRegistration.Service;
using BankSlipRegistration.Service.Events;
using BankSlipRegistration.Service.Services;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<IAlertService, AlertService>();

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

        config.ReceiveEndpoint(queue ?? "UniqueQueueName", e =>
        {
            e.Consumer<BankSlipRegistrationConsumer>(context);
        });

        config.ConfigureEndpoints(context);
    });

    x.AddConsumer<BankSlipRegistrationConsumer>();
});


var host = builder.Build();
host.Run();
