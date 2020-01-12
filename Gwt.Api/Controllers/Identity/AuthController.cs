using System.Threading.Tasks;
using Gwt.Api.Controllers.Base;
using Gwt.Api.Dto.Requests;
using Gwt.Api.Models.Configuration;
using Gwt.Api.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Gwt.Api.Controllers.Identity
{
  public class AuthController : BaseController
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByEmailAsync(loginRequest.UsernameOrEmail);
        if (user == null)
        {
          user = await _userManager.FindByNameAsync(loginRequest.UsernameOrEmail);
        }

        if (user == null)
        {
          return BadRequest("There is no user with that data");
        }
        var canSignInResult = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
        if (canSignInResult.Succeeded)
        {
          return Ok("Login action");
        };

        return BadRequest("Wrong creds");
      }

      return BadRequest("Model not valid");

    }
  }
}