using System.Runtime.Serialization;
using Unibus.Abstractions;

namespace Unibus.Configurations;

public class UnibusConfiguration
{
    private readonly List<Type> _consumers = [];
    
    private readonly List<Type> _events = [];
    
    private readonly List<Type> _commands = [];
    
    private readonly List<(Type consumerType, Type messageType)> _consumerMessageBindings = [];
    
    internal ITransportConfiguration? TransportConfiguration { get; set; }
    
    internal IReadOnlyList<Type> Events => _events;
    
    internal IReadOnlyList<Type> Commands => _commands;
    
    internal IReadOnlyList<Type> Consumers => _consumers;
    
    internal IReadOnlyList<(Type consumerType, Type messageType)> ConsumerMessageBindings => _consumerMessageBindings;
    
    public void AddEvent<TEvent>()
    {
        _events.Add(typeof(TEvent));
    }
    
    public void AddCommand<TCommand>()
    {
        _commands.Add(typeof(TCommand));
    }
    
    public void AddConsumer<TConsumer>()
    {
        var consumerType = typeof(TConsumer);
        
        // Ensure the type implements IConsumer<>
        var consumerInterface = consumerType.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>));
        
        if (consumerInterface == null)
        {
            throw new InvalidOperationException($"{consumerType.Name} does not implement IConsumer<>.");
        }
        
        // Extract the TMessage type
        var messageType = consumerInterface.GetGenericArguments()[0];
        
        _consumers.Add(consumerType);
        _consumerMessageBindings.Add((consumerType, messageType));
    }
}