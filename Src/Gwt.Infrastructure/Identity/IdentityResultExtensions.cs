using System.Linq;
using Gwt.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Gwt.Infrastructure.Identity
{
  public static class IdentityResultExtensions
  {
    public static Result ToApplicationResult(this IdentityResult result)
    {
      return result.Succeeded
        ? Result.Success()
        : Result.Failure(result.Errors.Select(e => e.Description));
    }

    public static Result ToApplicationResult(this SignInResult result)
    {
      return result.Succeeded
        ? Result.Success()
        : Result.Failure();
    }
  }
}