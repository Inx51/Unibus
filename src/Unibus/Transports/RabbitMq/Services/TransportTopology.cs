using RabbitMQ.Client;
using Unibus.Abstractions;

namespace Unibus.Transports.RabbitMq.Services;

public class TransportTopology : ITransportTopology
{
    private readonly ConnectionManager _connectionManager;

    public TransportTopology(ConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }
    
    public async Task CreateEventTopologyAsync(string eventName, string consumerName)
    {
        var channel = await GetChannelAsync();
        
        var exchangeName = $"{eventName}.event";
        var exchangeTask = channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout, true, false, null);
        var queueName = $"{eventName}.{consumerName}.event";
        var queueTask = channel.QueueDeclareAsync(queueName, true, false, false, null);
        var bindingTask = channel.QueueBindAsync(queueName, exchangeName, string.Empty, null);
        
        Task.WaitAll([exchangeTask, queueTask, bindingTask]);
    }

    public async Task CreateCommandTopologyAsync(string commandName, string _)
    {
        var channel = await GetChannelAsync();
        
        var name = $"{commandName}.command";
        var exchangeTask = channel.ExchangeDeclareAsync(name, ExchangeType.Fanout, true, false, null);
        var queueTask = channel.QueueDeclareAsync(name, true, false, false, null);
        var bindingTask = channel.QueueBindAsync(name, name, string.Empty, null);

        Task.WaitAll([exchangeTask, queueTask, bindingTask]);
    }

    private async Task<IChannel> GetChannelAsync()
    {
        var connection = await _connectionManager.GetConnectionAsync();
        return await connection.CreateChannelAsync();
    }
}