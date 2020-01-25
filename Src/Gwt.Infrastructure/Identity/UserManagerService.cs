using System;
using Microsoft.AspNetCore.Identity;
using Gwt.Application.Common.Models;
using System.Threading.Tasks;
using Gwt.Application.Common.Interfaces;
using System.Linq;

namespace Gwt.Infrastructure.Identity
{
  public class UserManagerService : IUserManager
  {
    private readonly UserManager<ApplicationUser> _userManager;

    public UserManagerService(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

    public async Task<(Result result, Guid userId)> CreateUserAsync(string userName, string password)
    {
      var user = new ApplicationUser
      {
        UserName = userName,
        Email = userName
      };
      var result = await _userManager.CreateAsync(user, password);

      return (result.ToApplicationResult(), user.Id);
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
      var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
      if (user != null)
      {
        return await DeleteUserAsync(user);
      }
      return Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
      var result = await _userManager.DeleteAsync(user);
      return result.ToApplicationResult();
    }

    public async Task<IApplicationUser> FindByEmailAsync(string email)
    {
      return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IApplicationUser> FindByIdAsync(Guid id)
    {
      return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<IApplicationUser> FindByNameAsync(string userName)
    {
      return await _userManager.FindByNameAsync(userName);
    }
  }
}