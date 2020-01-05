using System;
using Gwt.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gwt.Models
{
  public class GwtContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
  {
    public GwtContext(DbContextOptions<GwtContext> options) : base(options) { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
  }
}