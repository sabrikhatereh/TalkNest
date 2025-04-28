using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TalkNest.Application.Abstractions.DbContexts
{
    public interface IApplicationWriteDbContext
    {
        //IDbConnection Connection { get; }

        DbSet<Core.Models.Post> Posts { get; }

        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
