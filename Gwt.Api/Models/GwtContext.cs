using System;
using Microsoft.EntityFrameworkCore;

namespace Gwt.Models
{
  public class GwtContext : DbContext
  {
    public GwtContext(DbContextOptions<GwtContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().HasData(new User { Id = Guid.NewGuid(), FirstName = "Johnny" }, new User { Id = Guid.NewGuid(), FirstName = "Carl" });
    }
  }
}