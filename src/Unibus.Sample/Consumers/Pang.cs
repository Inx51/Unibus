using System.Runtime.Serialization;
using Unibus.Abstractions;
using Unibus.Contexts;

namespace Unibus.Sample.Consumers;

public class Pang : IConsumer<Contracts.Commands.Pang>
{
    public Task ConsumeAsync(MessageContext<Contracts.Commands.Pang> context)
    {
        return Task.CompletedTask;
    }
}