using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TalkNest.Infrastructure.Persistence.DbContexts
{
    /// <summary>
    /// DesignTime Factory
    /// </summary>
    public class TalkNestWriteDbContextFactory : IDesignTimeDbContextFactory<TalkNestWriteDbContext>
    {
        public TalkNestWriteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TalkNestWriteDbContext>();
            optionsBuilder.UseNpgsql();

            return new TalkNestWriteDbContext(optionsBuilder.Options);
        }
    }

}
