using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Api.Models.Identity
{
  public class ApplicationUser : IdentityUser<Guid>
  { }
}