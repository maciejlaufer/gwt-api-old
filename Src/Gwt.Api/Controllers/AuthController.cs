using System.Threading.Tasks;
using Gwt.Api.Requests.Auth;
using Gwt.Application.Common.Interfaces;
using Gwt.Application.Users.Commands.LoginUser;
using Gwt.Application.Common.Exceptions;
using Gwt.Application.Users.Commands.RegisterUser;
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
    private readonly IJwtTokenService _jwtTokenService;
    public AuthController(
      IUserManager userManager,
      ISignInManager signInManager,
      IMediator mediator,
      IJwtTokenService jwtTokenService
    )
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _mediator = mediator;
      _jwtTokenService = jwtTokenService;

    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
    {
      if(registerRequest.Password != registerRequest.RepeatPassword)
      {
        throw new BadRequestException("Passwords have to match");
      }
      var command = new RegisterUserCommand(registerRequest.Email, registerRequest.Firstname, registerRequest.Lastname, registerRequest.Password);
      await _mediator.Send(command);
      return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
    {
      // await _mediator.Send(new LoginUserCommand(loginRequest.UsernameOrEmail, loginRequest.Password));
      var user = await _userManager.FindByEmailAsync(loginRequest.Email);

      if (user == null)
      {
        return BadRequest("There is no user with that data");
      }
      var canSignInResult = await _signInManager.CheckPasswordSignInByEmailAsync(user.Email, loginRequest.Password, false);
      if (canSignInResult.Succeeded)
      {
        var (tokenId, token) = await _jwtTokenService.GenerateJwtToken(user.Email);
        return Ok(token);
      };

      return BadRequest("Wrong creds");
    }
  }
}