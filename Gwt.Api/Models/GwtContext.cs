using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gwt.Models
{
  public class GwtContext : DbContext
  {
    public GwtContext(DbContextOptions<GwtContext> options, IConfiguration config) : base(options)
    {
      Configuration = config;
    }
    public IConfiguration Configuration { get; }
    public DbSet<User> Users { get; set; }
  }
}