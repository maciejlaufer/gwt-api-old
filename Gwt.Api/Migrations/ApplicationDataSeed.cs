using System;
using System.Linq;
using Gwt.Api.Models;
using Gwt.Api.Models.Configuration;
using Gwt.Api.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gwt.Api.Migrations
{
  internal static class ApplicationDataSeed
  {
    public static void Seed(
      IServiceProvider serviceProvider,
      ApplicationUserConfiguration userConfig,
      bool isDevelopment)
    {
      SeedRoles(serviceProvider);
      SeedUsers(serviceProvider, userConfig);
    }

    public static void SeedUsers(IServiceProvider serviceProvider, ApplicationUserConfiguration config)
    {
      var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      if (userManager.FindByEmailAsync(config.Email).Result == null)
      {
        ApplicationUser adminUser = new ApplicationUser
        {
          UserName = config.UserName,
          Email = config.Email
        };
        IdentityResult result = userManager.CreateAsync(adminUser, config.Password).Result;

        if (result.Succeeded)
        {
          userManager.AddToRoleAsync(adminUser, "SystemAdmin").Wait();
        }
      }
    }

    public static void SeedRoles(IServiceProvider serviceProvider)
    {
      var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
      var adminRoleName = "SystemAdmin";
      if (!roleManager.RoleExistsAsync(adminRoleName).Result)
      {
        ApplicationRole systemAdminRole = new ApplicationRole
        {
          Id = Guid.NewGuid(),
          Name = adminRoleName,
          NormalizedName = adminRoleName.ToUpper()
        };
        IdentityResult result = roleManager.CreateAsync(systemAdminRole).Result;
      }
    }
  }
}