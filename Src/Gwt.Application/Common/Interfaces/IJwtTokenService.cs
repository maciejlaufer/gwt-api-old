using System;
using System.Threading.Tasks;

namespace Gwt.Application.Common.Interfaces
{
  public interface IJwtTokenService
  {
    Task<(Guid, string)> GenerateJwtToken(string email);
  }
}