using Microsoft.Extensions.Hosting;
using Unibus.Extensions;
using Unibus.Transports.RabbitMq.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddUnibus(config =>
{
    config.UseRabbitMq(new Uri("amqp://guest:guest@localhost:5672/"));
    
    config.AddEvent<Unibus.Sample.Contracts.Events.Ping>();
    config.AddConsumer<Unibus.Sample.Consumers.Ping>();
    config.AddConsumer<Unibus.Sample.Consumers.PingA>();
    config.AddConsumer<Unibus.Sample.Consumers.PingB>();
    
    config.AddCommand<Unibus.Sample.Contracts.Commands.Pang>();
    config.AddConsumer<Unibus.Sample.Consumers.Pang>();
    config.AddConsumer<Unibus.Sample.Consumers.PangA>();
    config.AddConsumer<Unibus.Sample.Consumers.PangB>();
});

var app = builder.Build();

app.Run();