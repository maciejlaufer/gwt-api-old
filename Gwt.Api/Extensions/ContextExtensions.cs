using System.Linq;
using Gwt.Migrations;
using Gwt.Models;
using Microsoft.EntityFrameworkCore;

namespace Gwt.Extensions
{
  public static class ContextExtensions
  {
    public static bool AreAllMigrationsApplied(this GwtContext context)
    {
      return !context.Database.GetPendingMigrations().Any();
    }

    public static void EnsureSeeded(this GwtContext context, bool isDevelopment)
    {
      ApplicationDataSeed.Seed(context, isDevelopment);
    }
  }
}