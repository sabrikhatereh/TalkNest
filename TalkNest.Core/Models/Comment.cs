using TalkNest.Core.Abstractions.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace TalkNest.Core.Models
{
    public class Comment : IAuditableEntity
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        public Guid PostId { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; private set; }

        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public Post Post { get; private set; }

        public Comment(Guid id) { Id = id; }

        public static Comment Create(Guid id, Guid postId, string text)
        {
            var comment = new Comment(id)
            {
                PostId = postId,
                Text = text
            };

            return comment;
        }

        public void Update(string text)
        {
            Text = text;
        }
    }

}

