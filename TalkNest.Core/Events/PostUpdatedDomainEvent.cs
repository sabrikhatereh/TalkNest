using TalkNest.Core.Abstractions.Events;
using System;

namespace TalkNest.Core.Events
{
    public record PostUpdatedDomainEvent(Guid Id, Guid PostId, string title,
        string Content) : IDomainEvent;
}
