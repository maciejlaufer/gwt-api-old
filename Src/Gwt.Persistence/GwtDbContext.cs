using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gwt.Application.Common.Interfaces;
using Gwt.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Gwt.Domain.Common;
using Gwt.Common;

namespace Gwt.Persistence
{
  public class GwtDbContext : DbContext, IGwtDbContext
  {
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public GwtDbContext(DbContextOptions<GwtDbContext> options) : base(options) { }
    public GwtDbContext(DbContextOptions<GwtDbContext> options, IDateTime dateTime, ICurrentUserService currentUserService) : base(options)
    {
      _currentUserService = currentUserService;
      _dateTime = dateTime;
    }

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.CreatedBy = _currentUserService.UserId;
            entry.Entity.CreatedAt = _dateTime.UtcNow;
            break;
          case EntityState.Modified:
            entry.Entity.LastModifiedBy = _currentUserService.UserId;
            entry.Entity.LastModifiedAt = _dateTime.UtcNow;
            break;
        }
      }
      return base.SaveChangesAsync(cancellationToken);
    }
  }
}