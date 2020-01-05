using System;
using Gwt.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gwt.Models
{
  public class GwtContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
  {
    public GwtContext(DbContextOptions<GwtContext> options, IConfiguration config) : base(options)
    {
      Configuration = config;
    }
    public IConfiguration Configuration { get; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
  }
}