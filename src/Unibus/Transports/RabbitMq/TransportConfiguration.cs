using Microsoft.Extensions.DependencyInjection;
using Unibus.Abstractions;
using Unibus.Transports.RabbitMq.Services;

namespace Unibus.Transports.RabbitMq;

public class TransportConfiguration : ITransportConfiguration
{
    public Uri ConnectionUri { get; set; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ConnectionManager>(_ => new ConnectionManager(ConnectionUri));
        services.AddTransient<ITransportTopology, TransportTopology>();
    }
}