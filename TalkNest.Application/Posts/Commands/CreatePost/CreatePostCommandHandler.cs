using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Core;
using TalkNest.Core.Models;
using AutoMapper;
using TalkNest.Core.Abstractions;
using Microsoft.Extensions.Logging;

namespace TalkNest.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostViewModel?>
    {
        private readonly IPostCommandRepository _PostCommandRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(IPostCommandRepository PostCommandRepository, IMapper mapper,
            IMediator mediator, IUnitOfWork unitOfWork)
        {
            _PostCommandRepository = PostCommandRepository;
            _mapper = mapper;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<PostViewModel?> Handle(CreatePostCommand command, CancellationToken cancellationToken)
        {
            var post = Post.Create(command.Id, command.Title, command.Content);
            await _PostCommandRepository.Add(post);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in post.GetDomainEvents())
                await _mediator.Publish(domainEvent, cancellationToken);

            // Clear domain events after publishing
            post.ClearDomainEvents();
            return _mapper.Map<Post, PostViewModel>(post);

        }
    }
}