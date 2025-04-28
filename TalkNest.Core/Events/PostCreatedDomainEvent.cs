using TalkNest.Core.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TalkNest.Core.Events
{
    public record PostCreatedDomainEvent(Guid Id,
        Guid PostId,
        string title,
        string Content) : IDomainEvent;

    
}
