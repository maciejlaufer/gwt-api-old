using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gwt.Extensions;
using Gwt.Migrations;
using Gwt.Models;
using Gwt.Models.Configuration;
using Gwt.Models.Identity;
using Gwt.Repositories.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Gwt.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<IUserRepository, UserRepository>();

      services.AddControllers();

      services.AddDbContext<GwtContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString(nameof(GwtContext))));


      services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<GwtContext>()
        .AddDefaultTokenProviders();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
      //database migrations
      using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<GwtContext>();
        if (!context.AreAllMigrationsApplied())
        {
          context.Database.Migrate();
        }

        var userConfiguration = Configuration.GetSection("ApplicationAdminUser").Get<ApplicationUserConfiguration>();
        ApplicationDataSeed.Seed(context, userManager, roleManager, userConfiguration, env.IsDevelopment());
      }

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();
      }

      // app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();
      app.UseAuthentication();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
