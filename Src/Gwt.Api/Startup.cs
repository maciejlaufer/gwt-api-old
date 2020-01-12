using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwt.Api.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Gwt.Persistence;
using Gwt.Domain.Entities;
using Gwt.Persistence.Extensions;
using Gwt.Api.Configuration;

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
      services.AddPersistence(Configuration);

      services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<GwtDbContext>()
        .AddDefaultTokenProviders();

      var jwtSettingsSection = Configuration.GetSection("JwtSettings");
      var _jwtSettings = jwtSettingsSection.Get<JwtSettings>();
      services.Configure<JwtSettings>(jwtSettingsSection);

      services.AddControllers();

      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // <= remove defaulta claims
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(config =>
      {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
          ValidIssuer = _jwtSettings.Issuer,
          ValidAudience = _jwtSettings.Issuer,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
          ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      //database migrations
      using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<GwtDbContext>();
        if (!context.AreAllMigrationsApplied())
        {
          context.Database.Migrate();
        }

        ApplicationDataSeed.Seed(serviceScope.ServiceProvider, Configuration.GetSection("ApplicationAdminUser"), env.IsDevelopment());
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
