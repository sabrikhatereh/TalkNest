using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace TalkNest.Application.Posts.Commands.UpdatePost
{
    public record UpdatePostCommand(Guid Id, string Title, string Content) : IRequest<PostViewModel>;
}
