using System;

namespace Gwt.Application.Common.Interfaces
{
  public interface IApplicationUser
  {
    Guid Id { get; set; }
    string Email { get; set; }
    string UserName { get; set; }
  }
}