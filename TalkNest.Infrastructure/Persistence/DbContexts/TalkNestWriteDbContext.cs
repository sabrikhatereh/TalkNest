using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using TalkNest.Application.Abstractions.DbContexts;
using System;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Core.Models;
using TalkNest.Core.Abstractions.Models;
using System.Linq;
using TalkNest.Application.Exceptions;

namespace TalkNest.Infrastructure.Persistence.DbContexts
{
    public class TalkNestWriteDbContext : DbContext, IApplicationWriteDbContext
    {
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public bool HasChanges => ChangeTracker.HasChanges();
        public TalkNestWriteDbContext(DbContextOptions<TalkNestWriteDbContext> options)
                                                    : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = currentPostService.PostId; //read current Post from Service
                        entry.Entity.CreatedOnUtc = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        // entry.Entity.LastModifiedBy = currentPostService.PostId; //read current Post from Service
                        entry.Entity.ModifiedOnUtc = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Post)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete comments
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId);
            });
            base.OnModelCreating(modelBuilder);
        }


    }
}
