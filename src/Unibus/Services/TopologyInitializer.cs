using Unibus.Abstractions;

namespace Unibus.Services;

public class TopologyInitializer
{
    private readonly ITransportTopology _transportTopology;

    public TopologyInitializer(ITransportTopology transportTopology)
    {
        _transportTopology = transportTopology;
    }
    
    public async Task CreateEventTopologyAsync(Type eventType, Type consumerType) => await _transportTopology.CreateEventTopologyAsync(GetTypeName(eventType),GetTypeName(consumerType));

    public async Task CreateCommandTopologyAsync(Type commandType, Type consumerType) => await _transportTopology.CreateCommandTopologyAsync(GetTypeName(commandType),GetTypeName(consumerType));
    
    private static string GetTypeName(Type type) => type.Name;
}