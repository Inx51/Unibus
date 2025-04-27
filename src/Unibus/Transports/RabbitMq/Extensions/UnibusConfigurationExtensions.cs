using Unibus.Configurations;

namespace Unibus.Transports.RabbitMq.Extensions;

public static class UnibusConfigurationExtensions
{
    public static void UseRabbitMq(this UnibusConfiguration configuration, Uri uri)
    {
        configuration.TransportConfiguration = new TransportConfiguration
        {
            ConnectionUri = uri
        };
    }
}