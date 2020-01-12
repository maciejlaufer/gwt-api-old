using Microsoft.EntityFrameworkCore;

namespace Gwt.Persistence
{
  public class GwtDbContextFactory : DesignTimeDbContextFactoryBase<GwtDbContext>
  {
    protected override GwtDbContext CreateNewInstance(DbContextOptions<GwtDbContext> options)
    {
      return new GwtDbContext(options);
    }
  }
}