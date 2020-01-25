using System;
using System.Threading.Tasks;
using Gwt.Application.Common.Models;

namespace Gwt.Application.Common.Interfaces
{
  public interface IUserManager
  {
    Task<(Result result, Guid userId)> CreateUserAsync(string userName, string password);
    Task<Result> DeleteUserAsync(Guid userId);
    Task<IApplicationUser> FindByEmailAsync(string email);
    Task<IApplicationUser> FindByNameAsync(string userName);
    Task<IApplicationUser> FindByIdAsync(Guid id);
  }
}