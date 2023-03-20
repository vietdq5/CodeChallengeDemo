using CodeChallenge.Domain.Common;
using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Infrastructure.Persistence;

public class CodeChallengeContext : DbContext
{
    public CodeChallengeContext(DbContextOptions<CodeChallengeContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "admin";
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "admin";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}