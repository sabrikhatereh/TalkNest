using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TalkNest.Core.Abstractions;
using TalkNest.Core;

namespace TalkNest.Application.Comments
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Guid?>
    {
        private readonly IPostCommandRepository _PostCommandRepository;
        private readonly IPostQueryRepository _PostQueryRepository;

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommentHandler(IPostCommandRepository postCommandRepository, IPostQueryRepository postQueryRepository,
            IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _PostCommandRepository = postCommandRepository;
            _PostQueryRepository = postQueryRepository;
            _mapper = mapper;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid?> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var post = await _PostQueryRepository.Get(command.PostId);
            post.AddComment(command.Id, command.Text);
            _PostCommandRepository.AddComment(post);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in post.GetDomainEvents())
                await _mediator.Publish(domainEvent, cancellationToken);

            // Clear domain events after publishing
            post.ClearDomainEvents();
            return command.Id;

        }
    }
}
