using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Core.Events;
using TalkNest.Core.Abstractions.Events;
using EasyCaching.Core;

namespace TalkNest.Application.Posts.Commands.CreatePost
{
    public class PostCreatedEventHandler : IEventHandler<PostCreatedDomainEvent>
    {
        private readonly ILogger<PostCreatedEventHandler> logger;
       

        public PostCreatedEventHandler(ILogger<PostCreatedEventHandler> logger)
        {
            this.logger = logger;
        }

        public async Task Handle(PostCreatedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            logger.LogInformation($"PostId [{domainEvent.PostId}, created  Post {(domainEvent as IDomainEvent).OccurredOn}] .");
        }
    }
}
