using MediatR;

namespace TalkNest.Core.Abstractions.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
      where TEvent : IEvent
    {
    }

}