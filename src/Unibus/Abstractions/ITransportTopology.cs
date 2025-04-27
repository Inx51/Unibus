namespace Unibus.Abstractions;

public interface ITransportTopology
{
    Task CreateEventTopologyAsync(string eventName, string consumerName);
    
    Task CreateCommandTopologyAsync(string commandName, string consumerName);
}