using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Gwt.Persistence.Extensions
{
  public static class ContextExtensions
  {
    public static bool AreAllMigrationsApplied(this GwtDbContext context)
    {
      return !context.Database.GetPendingMigrations().Any();
    }
  }
}