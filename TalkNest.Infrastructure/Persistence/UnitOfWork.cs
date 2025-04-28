using TalkNest.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Infrastructure.Persistence.DbContexts;
using Microsoft.Extensions.Logging;

namespace TalkNest.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalkNestWriteDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(TalkNestWriteDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while saving changes to the database");
                throw;
            }
        }
    }
}
