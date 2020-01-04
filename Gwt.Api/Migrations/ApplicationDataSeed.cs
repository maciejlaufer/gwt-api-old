using System;
using System.Linq;
using Gwt.Models;
using Microsoft.Extensions.Configuration;

namespace Gwt.Migrations
{
  internal static class ApplicationDataSeed
  {
    public static void Seed(GwtContext context, bool isDevelopment)
    {
      SeedUsers(context);
    }

    public static void SeedUsers(GwtContext context)
    {
      if (!context.ApplicationUsers.Any(x => x.UserName == "sys_admin"))
      {
        context.ApplicationUsers.Add(new ApplicationUser()
        {
          Id = Guid.NewGuid(),
          UserName = "sys_admin"
        });
        context.SaveChanges();
      }
    }
  }
}