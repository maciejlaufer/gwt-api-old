using System.Linq;
using Gwt.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gwt.Api.Extensions
{
  public static class ContextExtensions
  {
    public static bool AreAllMigrationsApplied(this GwtContext context)
    {
      return !context.Database.GetPendingMigrations().Any();
    }
  }
}