using System;
using System.Threading.Tasks;
using Gwt.Application.Common.Models;

namespace Gwt.Application.Common.Interfaces
{
  public interface ISignInManager
  {
    Task<Result> CheckPasswordSignInByIdAsync(Guid userId, string password, bool lockoutOnFailure);

    Task<Result> CheckPasswordSignInByEmailAsync(string email, string password, bool lockoutOnFailure);

    Task<Result> CheckPasswordSignInByNameAsync(string userName, string password, bool lockoutOnFailure);
  }
}