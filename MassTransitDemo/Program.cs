using MassTransit;
using MassTransitDemo.Consumer;
using MassTransitDemo.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Retrieve RabbitMQ settings from configuration
var rabbitMqSettings = builder.Configuration.GetSection("EventBus");

// Configure MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PingConsumer>();

    // Configure RabbitMQ
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqSettings["host"], "/", h =>
        {
            h.Username(rabbitMqSettings["user"]);
            h.Password(rabbitMqSettings["pass"]);
        });

        cfg.ConfigureEndpoints(context);

        
    });
});

builder.Services.AddHostedService<PingPublisher>();

var app = builder.Build();
app.Run();
