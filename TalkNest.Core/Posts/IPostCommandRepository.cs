using TalkNest.Core.Models;
using System.Threading.Tasks;

namespace TalkNest.Core
{
    public interface IPostCommandRepository
    {
        Task Add(Post Post);
        void Update(Post Post);
        void Delete(Post Post);
        void AddComment(Post Post);
    }

}