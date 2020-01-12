using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gwt.Domain.Entities;
using Gwt.Application.Common.Interfaces;

namespace Gwt.Persistence
{
  public class GwtDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IGwtDbContext
  {
    public GwtDbContext(DbContextOptions<GwtDbContext> options) : base(options) { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRole { get; set; }
  }
}