using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Application.Posts.Queries;
using TalkNest.Core;
using TalkNest.Core.Models;
using AutoMapper;
using TalkNest.Application.Posts;
using System.Collections.Generic;

namespace TalkNest.Application.TalkNests.Queries
{
    public class ListPostsHandler : IRequestHandler<ListPostsQuery, List<PostViewModel>>
    {
        private readonly IPostQueryRepository _PostQueryRepository;
        private readonly IMapper _mapper;

        public ListPostsHandler(IPostQueryRepository postQueryRepository, IMapper mapper)
        {
            _PostQueryRepository = postQueryRepository;
            _mapper = mapper;
        }

        public async Task<List<PostViewModel>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _PostQueryRepository.GetAll();
            return _mapper.Map<IEnumerable<Post>, List<PostViewModel>>(posts);
        }
    }
}
