using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gwt.Application.Common.Interfaces;
using Gwt.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Gwt.Domain.Common;

namespace Gwt.Persistence
{
  public class GwtDbContext : DbContext, IGwtDbContext
  {
    public GwtDbContext(DbContextOptions<GwtDbContext> options) : base(options) { }

    public DbSet<Profile> Profiles { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.CreatedBy = ""; // TODO: add current user service
            entry.Entity.CreatedAt = DateTime.UtcNow; // TODO: add datetime service
            break;
          case EntityState.Modified:
            entry.Entity.LastModifiedBy = "";
            entry.Entity.LastModifiedAt = DateTime.UtcNow;
            break;
        }
      }
      return base.SaveChangesAsync(cancellationToken);
    }
  }
}