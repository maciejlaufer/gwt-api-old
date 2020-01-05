using System.Threading.Tasks;
using Gwt.Controllers.Base;
using Gwt.Models.Configuration;
using Gwt.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Gwt.Controllers.Auth
{
  public class AuthController : BaseController
  {
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthController(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

    [Route("login")]
    public async Task<IActionResult> Login()
    {
      return Ok("Login action");
    }
  }
}