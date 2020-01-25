using System;
using Gwt.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Infrastructure.Identity
{
  public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
  { }
}