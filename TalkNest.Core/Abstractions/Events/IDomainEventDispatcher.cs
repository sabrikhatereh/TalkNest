using System.Threading.Tasks;

namespace TalkNest.Core.Abstractions.Events
{
    public interface IDomainEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent[] domainEvent) where TEvent : IEvent;
    }
}