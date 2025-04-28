using TalkNest.Core.Abstractions.Models;
using TalkNest.Core.Events;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace TalkNest.Core.Models
{
    public class Post : Aggregate, IAuditableEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; private set; }

        [Required]
        public string Content { get; private set; }

        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public List<Comment> Comments { get; private set; } = new();

        public Post(Guid id) : base(id) { }


        public static Post Create(Guid id, string title, string content)
        {
            var post = new Post(id)
            {
                Title = title,
                Content = content
            };

            var @event = new PostCreatedDomainEvent(Guid.NewGuid(), post.Id, post.Title, post.Content);

            post.AddDomainEvent(@event);
            return post;
        }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
            AddDomainEvent(new PostUpdatedDomainEvent(Guid.NewGuid(), Id,
           Title,
           Content));
        }

        public void AddComment(Guid commentId, string text)
        {
            var comment = Comment.Create(commentId, Id, text);
            Comments.Add(comment);
            AddDomainEvent(new CommentAddedDomainEvent(Guid.NewGuid(), CommentId: comment.Id, PostId: Id,
                comment.Text));
        }
        // this approach is suitable when have limited number of comments
        public void UpdateComment(Guid commentId, string newText)
        {
            var comment = Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
                throw new Exception("Comment not found");

            comment.Update(newText);

            // maybe update Post’s metadata or raise a domain event
            AddDomainEvent(new CommentUpdatedDomainEvent(Guid.NewGuid(), CommentId: comment.Id, PostId: Id,
                comment.Text));
        }
    }
}

