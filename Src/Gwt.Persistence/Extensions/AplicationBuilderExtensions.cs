using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Gwt.Persistence.Extensions
{
  public static class AplicationBuilderExtensions
  {
    public static void InitializePersistence(this IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<GwtDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
          context.Database.Migrate();
        }
      }
    }
  }
}