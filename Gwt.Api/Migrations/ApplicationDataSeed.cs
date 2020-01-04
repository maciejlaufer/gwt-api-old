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
      if (!context.Users.Any(x => x.Username == "sys_admin"))
      {
        context.Users.Add(new User()
        {
          Id = Guid.NewGuid(),
          Username = "sys_admin"
        });
        context.SaveChanges();
      }
    }
  }
}