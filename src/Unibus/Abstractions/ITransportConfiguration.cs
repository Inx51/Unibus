using Microsoft.Extensions.DependencyInjection;

namespace Unibus.Abstractions;

public interface ITransportConfiguration
{
    void ConfigureServices(IServiceCollection services);
}