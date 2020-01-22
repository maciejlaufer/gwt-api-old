using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gwt.Infrastructure.Identity
{
  public static class AppInitializer
  {
    public static void InitializeIdentity(this IApplicationBuilder app, IConfiguration configuration, bool isDevelopment)
    {
      using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
          context.Database.Migrate();
        }

        ApplicationDataSeed.Seed(serviceScope.ServiceProvider, configuration.GetSection("ApplicationAdminUser"), isDevelopment);
      }
    }
  }
}