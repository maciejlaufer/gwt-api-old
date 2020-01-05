using System.Linq;
using Gwt.Migrations;
using Gwt.Models;
using Gwt.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gwt.Extensions
{
  public static class ContextExtensions
  {
    public static bool AreAllMigrationsApplied(this GwtContext context)
    {
      return !context.Database.GetPendingMigrations().Any();
    }
  }
}