using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Gwt.Persistence;
using Gwt.Api.Configuration;
using Gwt.Application;
using Gwt.Infrastructure;
using Gwt.Infrastructure.Identity;

namespace Gwt.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;
      Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddInfrastructure(Configuration, Environment);
      services.AddPersistence(Configuration);
      services.AddApplication();

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
      app.InitializeIdentity(Configuration, env.IsDevelopment());
      app.InitializePersistence();

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
