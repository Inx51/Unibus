using Microsoft.Extensions.Hosting;
using Unibus.Configurations;
using Unibus.Services;

namespace Unibus.Workers;

public class Initialization : IHostedService
{
    private readonly UnibusConfiguration _configuration;
    private readonly TopologyInitializer _topologyInitializer;
    
    public Initialization
    (
        UnibusConfiguration configuration,
        TopologyInitializer topologyInitializer
    )
    {
        _configuration = configuration;
        _topologyInitializer = topologyInitializer;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        SetupTopology();

        return Task.CompletedTask;
    }

    private void SetupTopology()
    {
        var tasks = new List<Task>();
        
        foreach (var bindings in _configuration.ConsumerMessageBindings)
        {
            if (_configuration.Commands.Contains(bindings.messageType))
                tasks.Add(_topologyInitializer.CreateCommandTopologyAsync(bindings.messageType, bindings.consumerType));

            if (_configuration.Events.Contains(bindings.messageType))
                tasks.Add(_topologyInitializer.CreateEventTopologyAsync(bindings.messageType, bindings.consumerType));
        }
        
        Task.WaitAll(tasks.ToArray());
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}