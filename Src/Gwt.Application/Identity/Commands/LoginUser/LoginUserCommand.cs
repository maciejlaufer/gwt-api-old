using System;
using System.Threading;
using System.Threading.Tasks;
using Gwt.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Application.Identity.Commands.LoginUser
{
  public class LoginUserCommand : IRequest
  {
    public string UsernameOrEmail { get; private set; }
    public string Password { get; private set; }

    public LoginUserCommand(string usernameOrPassword, string password)
    {
      UsernameOrEmail = usernameOrPassword;
      Password = password;
    }

    public class Handler : IRequestHandler<LoginUserCommand>
    {
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly SignInManager<ApplicationUser> _signInManager;
      public Handler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
      {
        _userManager = userManager;
        _signInManager = signInManager;
      }
      public async Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
      {
        Console.WriteLine(request.UsernameOrEmail);
        return Unit.Value;
      }
    }
  }
}