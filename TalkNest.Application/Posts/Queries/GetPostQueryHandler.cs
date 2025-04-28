using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Application.Posts.Queries;
using TalkNest.Core;
using TalkNest.Core.Models;
using AutoMapper;
using TalkNest.Application.Posts;
using System;
using TalkNest.Application.Exceptions;

namespace TalkNest.Application.TalkNests.Queries
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostViewModel?>
    {
        private readonly IPostQueryRepository _PostQueryRepository;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(IPostQueryRepository PostQueryRepository, IMapper mapper)
        {
            _PostQueryRepository = PostQueryRepository;
            _mapper = mapper;
        }

        public async Task<PostViewModel?> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var Post = await _PostQueryRepository.Get(request.Id);
            if (Post == null)
            {
                throw new NotFoundException($"Post with id {request.Id} not found.");
            }
            return _mapper.Map<Post, PostViewModel>(Post);
        }

    }
}
