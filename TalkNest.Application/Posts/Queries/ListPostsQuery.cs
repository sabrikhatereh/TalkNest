using MediatR;
using System.Collections.Generic;

namespace TalkNest.Application.Posts.Queries
{
    public record ListPostsQuery() : IRequest<List<PostViewModel>>;

}
