using System.Threading.Tasks;
using Gwt.Api.Dto.Requests;
using Gwt.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Gwt.Application.Identity.Commands.LoginUser;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Api.Controllers
{
  public class AuthController : BaseController
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMediator _mediator;
    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMediator mediator)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _mediator = mediator;

    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
    {
      if (ModelState.IsValid)
      {
        await _mediator.Send(new LoginUserCommand(loginRequest.UsernameOrEmail, loginRequest.Password));
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