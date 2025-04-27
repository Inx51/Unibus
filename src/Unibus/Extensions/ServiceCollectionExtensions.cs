using Microsoft.Extensions.DependencyInjection;
using Unibus.Configurations;
using Unibus.Services;
using Unibus.Workers;

namespace Unibus.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUnibus(this IServiceCollection serviceCollection, Action<UnibusConfiguration> configuration)
    {
        var config = new UnibusConfiguration();
        configuration(config);
        
        config.TransportConfiguration.ConfigureServices(serviceCollection);
        
        serviceCollection.AddTransient<TopologyInitializer>();
        serviceCollection.AddHostedService<Initialization>
        (
            sp => new Initialization
            (
                config,
                sp.GetRequiredService<TopologyInitializer>()
            )
        );
        
        return serviceCollection;
    }
}