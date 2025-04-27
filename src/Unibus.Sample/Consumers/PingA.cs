using System.Runtime.Serialization;
using Unibus.Abstractions;
using Unibus.Contexts;

namespace Unibus.Sample.Consumers;

public class PingA : IConsumer<Contracts.Events.Ping>
{
    public Task ConsumeAsync(MessageContext<Contracts.Events.Ping> context)
    {
        return Task.CompletedTask;
    }
}