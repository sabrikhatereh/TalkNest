using TalkNest.Core;
using TalkNest.Core.Models;
using TalkNest.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TalkNest.Infrastructure.Repositories.CommandRepositories
{
    public class PostCommandRepository : IPostCommandRepository
    {
        private readonly TalkNestWriteDbContext db;

        public PostCommandRepository(TalkNestWriteDbContext db)
        {
            this.db = db;
        }

        public async Task Add(Core.Models.Post Post)
        {
            await db.Posts.AddAsync(Post);
        }

        public void Delete(Core.Models.Post Post)
        {
            db.Posts.Remove(Post);
        }

        public void Update(Core.Models.Post Post)
        {
            db.Posts.Update(Post);
        }
        public void AddComment(Core.Models.Post Post)
        {
            db.Posts.Update(Post);
            db.Comments.Add(Post.Comments[0]);
        }
    }
}
