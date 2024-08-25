using MassTransit;
using MassTransitDemo.Publisher;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHostedService<PingPublisher>();

builder.Services.AddMassTransit(opt =>
{
    opt.AddConsumers(typeof(Program).Assembly);
    opt.UsingInMemory ((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app  = builder.Build();

app.Run();
