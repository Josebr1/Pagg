using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();