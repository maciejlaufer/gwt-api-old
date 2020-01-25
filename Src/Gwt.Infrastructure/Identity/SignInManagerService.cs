using System;
using System.Threading.Tasks;
using Gwt.Application.Common.Interfaces;
using Gwt.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Infrastructure.Identity
{
  public class SignInManagerService : ISignInManager
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManger;
    public SignInManagerService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
      _signInManager = signInManager;
      _userManger = userManager;
    }

    public async Task<Result> CheckPasswordSignInByIdAsync(Guid userId, string password, bool lockoutOnFailure)
    {
      ApplicationUser user = await _userManger.FindByIdAsync(userId.ToString());
      if (user == null)
      {
        // throw user does not exists exception
      }
      return await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }

    public async Task<Result> CheckPasswordSignInByEmailAsync(string email, string password, bool lockoutOnFailure)
    {
      ApplicationUser user = await _userManger.FindByEmailAsync(email);
      if (user == null)
      {
        // throw user does not exists exception
      }
      return await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }

    public async Task<Result> CheckPasswordSignInByNameAsync(string userName, string password, bool lockoutOnFailure)
    {
      ApplicationUser user = await _userManger.FindByNameAsync(userName);
      if (user == null)
      {
        // throw user does not exists exception
      }
      return await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }

    private async Task<Result> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure)
    {
      var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
      return result.ToApplicationResult();
    }
  }
}