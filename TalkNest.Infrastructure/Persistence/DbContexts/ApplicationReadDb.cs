using Microsoft.EntityFrameworkCore;
using TalkNest.Application.Abstractions.DbContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using System.Linq;

namespace TalkNest.Infrastructure.Persistence.DbContexts
{
    public class ApplicationReadDb : IApplicationReadDb
    {
        private readonly DbContext db;

        public ApplicationReadDb(TalkNestWriteDbContext db)
        {
            this.db = db;
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IQueryable<T>> include = null,
            CancellationToken cancellationToken = default)
            where T : class
        {

            IQueryable<T> query = db.Set<T>();

            if (predicate != null)
                query.Where(predicate);

            if (include != null)
                query = include(query);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(CancellationToken cancellationToken = default)
           where T : class
           => await db.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T> QueryFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate,
             Func<IQueryable<T>, IQueryable<T>> include = null,
            CancellationToken cancellationToken = default)
            where T : class
        {

            IQueryable<T> query = db.Set<T>().AsNoTracking();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<T> QuerySingleAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class
         => await db.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);

        public async Task<IReadOnlyList<TResult>> QueryAsync<T, TResult>(Expression<Func<T, bool>> predicate,
                                                                         Expression<Func<T, TResult>> selector,
                                                                         Func<IQueryable<T>, IQueryable<T>> include = null,
                                                                         CancellationToken cancellationToken = default) where T : class
        {
            return await db.Set<T>()
                .Where(predicate)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

    }

}

