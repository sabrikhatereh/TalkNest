using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Core.Events;
using TalkNest.Core.Abstractions.Events;

namespace TalkNest.Application.Posts.Commands.UpdatePost
{
    public class PostUpdatedEventHandler : IEventHandler<PostUpdatedDomainEvent>
    {
        private readonly ILogger<PostUpdatedEventHandler> logger;
       

        public PostUpdatedEventHandler(ILogger<PostUpdatedEventHandler> logger)
        {
            this.logger = logger;
        }

        public async Task Handle(PostUpdatedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            logger.LogInformation($"PostId [{domainEvent.PostId}, is updated at {(domainEvent as IDomainEvent).OccurredOn}] .");
        }
    }
}
