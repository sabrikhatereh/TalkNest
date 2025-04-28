using System.Collections.Generic;
using System.Threading.Tasks;

namespace TalkNest.Core.Abstractions.Services
{
    public interface IForbidWords
    {
        Task<List<string>> LoadForbidWords();
    }
}
