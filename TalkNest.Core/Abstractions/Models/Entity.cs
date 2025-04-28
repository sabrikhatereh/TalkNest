using System;
using System.ComponentModel.DataAnnotations;

namespace TalkNest.Core.Abstractions.Models
{
    public abstract class Entity 
    {
        protected Entity(Guid id)
        {
            Id = id;
        }
        [Key]
        public Guid Id { get; private set; }


    }
}