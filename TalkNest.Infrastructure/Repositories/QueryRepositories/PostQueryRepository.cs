using TalkNest.Application.Abstractions.DbContexts;
using TalkNest.Core;
using TalkNest.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TalkNest.Infrastructure.Repositories.QueryRepositories
{
    public class PostQueryRepository : IPostQueryRepository
    {
        private readonly IApplicationReadDb db;
        public PostQueryRepository(IApplicationReadDb db)
        {
            this.db = db;
        }


        public async Task<Post> Get(Guid Id)
        => await db.QueryFirstOrDefaultAsync<Post>(x => x.Id == Id,
             q => q.Include(p => p.Comments));


        public async Task<IEnumerable<Post>> GetAll()
         => await db.QueryAsync<Post>( include: q => q.Include(p => p.Comments));

        public async Task<Post> GetWithCommentId(Guid postId, Guid CommentId)
         => await db.QueryFirstOrDefaultAsync<Post>(x => x.Id == postId,
             q => q.Include(p => p.Comments.Where(x => x.Id == CommentId)));

    }

}
