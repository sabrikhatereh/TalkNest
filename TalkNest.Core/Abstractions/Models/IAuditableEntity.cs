using System;

namespace TalkNest.Core.Abstractions.Models
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        string LastModifiedBy { get; set; }
        DateTime CreatedOnUtc { get; set; }
        DateTime? ModifiedOnUtc { get; set; }
    }
}