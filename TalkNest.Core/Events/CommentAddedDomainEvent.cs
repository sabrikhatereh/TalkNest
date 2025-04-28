using TalkNest.Core.Abstractions.Events;
using System;

namespace TalkNest.Core.Events
{
    public record CommentAddedDomainEvent(Guid Id, Guid CommentId, Guid PostId,
        string Content) : IDomainEvent;

    public record CommentUpdatedDomainEvent(Guid Id, Guid CommentId, Guid PostId,
        string Content) : IDomainEvent;
}
