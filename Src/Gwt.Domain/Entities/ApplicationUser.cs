using System;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Domain.Entities
{
  public class ApplicationUser : IdentityUser<Guid>
  { }
}