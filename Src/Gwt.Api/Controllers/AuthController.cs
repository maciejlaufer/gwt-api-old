using System.Threading.Tasks;
using Gwt.Api.Dto.Requests;
using Gwt.Application.Common.Interfaces;
using Gwt.Application.Users.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Gwt.Api.Controllers
{
  public class AuthController : BaseController
  {
    private readonly IUserManager _userManager;
    private readonly ISignInManager _signInManager;
    private readonly IMediator _mediator;
    public AuthController(IUserManager userManager, ISignInManager signInManager, IMediator mediator)
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
        var canSignInResult = await _signInManager.CheckPasswordSignInByEmailAsync(user.Email, loginRequest.Password, false);
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