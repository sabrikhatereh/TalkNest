using System;
using Xunit;
using System.Linq;
using TalkNest.Core.Models;
using TalkNest.Core.Events;
using Shouldly;

namespace TalkNest.UnitTests
{
    public class PostTests
    {
        [Fact]
        public void Create_Should_Initialize_Post_With_Provided_Values()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var title = "My Title";
            var content = "My Content";

            // Act
            var post = Post.Create(postId, title, content);

            // Assert
            post.ShouldNotBeNull();
            post.Id.ShouldBe(postId);
            post.Title.ShouldBe(title);
            post.Content.ShouldBe(content);
            post.Comments.ShouldBeEmpty();
            post.GetDomainEvents().OfType<PostCreatedDomainEvent>().Count().ShouldBe(1);
        }

        [Fact]
        public void Update_Should_Update_Post_Fields_And_Raise_Domain_Event()
        {
            // Arrange
            var post = Post.Create(Guid.NewGuid(), "Old Title", "Old Content");

            // Act
            post.Update("New Title", "New Content");

            // Assert
            post.Title.ShouldBe("New Title");
            post.Content.ShouldBe("New Content");
            post.GetDomainEvents().OfType<PostUpdatedDomainEvent>().Count().ShouldBe(1);
        }

        [Fact]
        public void AddComment_Should_Add_Comment_And_Raise_Domain_Event()
        {
            // Arrange
            var post = Post.Create(Guid.NewGuid(), "Title", "Content");
            var commentId = Guid.NewGuid();
            var text = "This is a comment";

            // Act
            post.AddComment(commentId, text);

            // Assert
            post.Comments.Count.ShouldBe(1);
            post.Comments.First().Text.ShouldBe(text);
            post.GetDomainEvents().OfType<CommentAddedDomainEvent>().Count().ShouldBe(1);
        }

        [Fact]
        public void UpdateComment_Should_Update_Comment_Text_And_Raise_Domain_Event()
        {
            // Arrange
            var post = Post.Create(Guid.NewGuid(), "Title", "Content");
            var commentId = Guid.NewGuid();
            post.AddComment(commentId, "Initial Text");

            // Act
            post.UpdateComment(commentId, "Updated Text");

            // Assert
            var comment = post.Comments.First(c => c.Id == commentId);
            comment.Text.ShouldBe("Updated Text");
            post.GetDomainEvents().OfType<CommentUpdatedDomainEvent>().Count().ShouldBe(1);
        }

        [Fact]
        public void UpdateComment_Should_Throw_Exception_When_Comment_Not_Found()
        {
            // Arrange
            var post = Post.Create(Guid.NewGuid(), "Title", "Content");

            // Act & Assert
            var ex = Should.Throw<Exception>(() => post.UpdateComment(Guid.NewGuid(), "Some Text"));
            ex.Message.ShouldBe("Comment not found");
        }
    }
   

}
