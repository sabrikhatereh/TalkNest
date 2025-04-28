using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Core.Abstractions.Models;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using TalkNest.Core.Models;
using System.Linq;

namespace TalkNest.Application.Abstractions.DbContexts
{
    public interface IApplicationReadDb
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(Expression<Func<T, bool>> predicate=null,
       Func<IQueryable<T>, IQueryable<T>> include = null,
       CancellationToken cancellationToken = default) where T : class;
        Task<IReadOnlyList<T>> QueryAsync<T>(CancellationToken cancellationToken = default) where T : class;

        Task<T> QueryFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> include = null,
            CancellationToken cancellationToken = default) where T : class;

        Task<T> QuerySingleAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class;
        Task<IReadOnlyList<TResult>> QueryAsync<T, TResult>(Expression<Func<T, bool>> predicate,
           Expression<Func<T, TResult>> selector,
           Func<IQueryable<T>, IQueryable<T>> include = null,
           CancellationToken cancellationToken = default) where T : class;
    }

}