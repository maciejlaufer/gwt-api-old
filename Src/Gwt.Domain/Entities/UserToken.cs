using System;
using Gwt.Domain.Common;

namespace Gwt.Domain.Entities
{
  public class UserToken : IEntity
  {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
  }
}