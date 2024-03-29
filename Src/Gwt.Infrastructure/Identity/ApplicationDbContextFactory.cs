using Microsoft.EntityFrameworkCore;

namespace Gwt.Infrastructure.Identity
{
  public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
  {
    protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
    {
      return new ApplicationDbContext(options);
    }
  }
}