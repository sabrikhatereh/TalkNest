using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace TalkNest.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand(Guid Id, string Title, string Content) : IRequest<PostViewModel>;

}
