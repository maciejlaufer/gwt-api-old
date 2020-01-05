using System;
using System.Linq;
using Gwt.Models;
using Gwt.Models.Configuration;
using Gwt.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Gwt.Migrations
{
  internal static class ApplicationDataSeed
  {
    public static void Seed(GwtContext context, bool isDevelopment)
    {
    }

    public static void SeedUsers(UserManager<ApplicationUser> userManager, ApplicationUserConfiguration config)
    {
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
  }
}