using System;

namespace TalkNest.Core.Abstractions.Models
{
    public interface ISoftDeletableEntity
    {
        DateTime? DeletedOnUtc { get; }
        bool Deleted { get; }
    }
}