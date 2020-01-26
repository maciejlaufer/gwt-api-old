using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Gwt.Application.Common.Interfaces;
using Gwt.Application.Common.Models;
using Gwt.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gwt.Infrastructure
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
      services.AddScoped<IUserManager, UserManagerService>();
      services.AddScoped<ISignInManager, SignInManagerService>();

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("GwtDbContext")));

      services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

      var jwtSettingsSection = configuration.GetSection("JwtSettings");
      var _jwtSettings = jwtSettingsSection.Get<JwtSettings>();
      services.Configure<JwtSettings>(jwtSettingsSection);

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

      return services;
    }
  }
}