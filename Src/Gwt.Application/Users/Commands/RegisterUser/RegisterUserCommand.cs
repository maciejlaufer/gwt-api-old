using System;
using System.Threading;
using System.Threading.Tasks;
using Gwt.Application.Common.Interfaces;
using Gwt.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Application.Users.Commands.RegisterUser
{
  public class RegisterUserCommand : IRequest
  {
    public string Email { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }    
    public string Password { get; private set; }

    public RegisterUserCommand(string email, string firstname, string lastname, string password)
    {
      Email = email;
      Firstname = firstname;
      Lastname = lastname;
      Password = password;
    }

    public class Handler : IRequestHandler<RegisterUserCommand>
    {
      private readonly IUserManager _userManager;
      private readonly ISignInManager _signInManager;
      private readonly IGwtDbContext _context;
      public Handler(IUserManager userManager, ISignInManager signInManager, IGwtDbContext context)
      {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
      }
      public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
      {
        Console.WriteLine(request.Email);
        return Unit.Value;
      }
    }
  }
}