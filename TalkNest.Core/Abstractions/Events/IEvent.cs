using MediatR;
using System;

namespace TalkNest.Core.Abstractions.Events
{
    public interface IDomainEvent: IEvent
    {
       public DateTime OccurredOn => DateTime.Now;

    }
    public interface IEvent : INotification
    {
    }

}