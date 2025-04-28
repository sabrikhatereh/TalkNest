using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Core;
using TalkNest.Core.Models;
using TalkNest.Application.Exceptions;
using AutoMapper;
using TalkNest.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TalkNest.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostViewModel?>
    {
        private readonly IPostCommandRepository _PostCommandRepository;
        private readonly IPostQueryRepository _PostQueryRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePostCommandHandler(IPostCommandRepository PostCommandRepository,
            IPostQueryRepository PostQueryRepository,
            IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _PostCommandRepository = PostCommandRepository;
            _PostQueryRepository = PostQueryRepository;
            _mapper = mapper;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<PostViewModel?> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
        {

            var Post = await _PostQueryRepository.Get(command.Id);

            if (Post is null)
                throw new NotFoundException($"Post with ID {command.Id} was not found.");

            Post.Update(command.Title, command.Content);
            _PostCommandRepository.Update(Post);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in Post.GetDomainEvents())
                await _mediator.Publish(domainEvent, cancellationToken);

            // Clear domain events after publishing
            Post.ClearDomainEvents();

            return _mapper.Map<Core.Models.Post, PostViewModel>(Post);
        }
    }
}
