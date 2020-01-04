using System;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Models
{
  public class ApplicationUser : IdentityUser<Guid>
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nickname { get; set; }
  }
}