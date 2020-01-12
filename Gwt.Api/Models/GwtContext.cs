using System;
using Gwt.Api.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gwt.Api.Models
{
  public class GwtContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
  {
    public GwtContext(DbContextOptions<GwtContext> options) : base(options) { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
  }
}