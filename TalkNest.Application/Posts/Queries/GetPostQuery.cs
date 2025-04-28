using MediatR;
using System;
using TalkNest.Application.Posts.Commands.CreatePost;
using TalkNest.Core.Abstractions.Services;
using TalkNest.Core.Models;

namespace TalkNest.Application.Posts.Queries
{
    public record GetPostQuery(Guid Id) : IRequest<PostViewModel>;
}