using Unibus.Contexts;

namespace Unibus.Abstractions;

public interface IConsumer<TMessage>
{
    Task ConsumeAsync(MessageContext<TMessage> context);
}