using Gwt.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gwt.Persistence
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddDbContext<GwtDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString(nameof(GwtDbContext))));

      services.AddScoped<IGwtDbContext>(provider => provider.GetService<GwtDbContext>());

      return services;
    }
  }
}