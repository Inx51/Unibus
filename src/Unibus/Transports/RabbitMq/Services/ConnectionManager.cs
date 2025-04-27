using RabbitMQ.Client;

namespace Unibus.Transports.RabbitMq.Services;

public class ConnectionManager
{
    private readonly Uri _connectionUri;
    private IConnection? _connection;
    
    public ConnectionManager(Uri connectionUri)
    {
        _connectionUri = connectionUri;
    }
    
    public async Task<IConnection> GetConnectionAsync()
    {
        if (_connection is not null)
            return _connection;
        
        var factory = new ConnectionFactory
        {
            Uri = _connectionUri
        };
        
        _connection = await factory.CreateConnectionAsync();
        
        return _connection;
    }
}