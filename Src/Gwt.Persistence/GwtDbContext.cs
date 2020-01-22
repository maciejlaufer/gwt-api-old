using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gwt.Application.Common.Interfaces;
using Gwt.Domain.Entities;

namespace Gwt.Persistence
{
  public class GwtDbContext : DbContext, IGwtDbContext
  {
    public GwtDbContext(DbContextOptions<GwtDbContext> options) : base(options) { }

    public DbSet<Profile> Profiles { get; set; }
  }
}