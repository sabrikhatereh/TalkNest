using TalkNest.Core.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace TalkNest.Core
{
    public interface IPostQueryRepository
    {
        Task<IEnumerable<Models.Post>> GetAll();
        Task<Models.Post> Get(Guid Id);
        Task<Post> GetWithCommentId(Guid postId, Guid CommentId);

    }

}