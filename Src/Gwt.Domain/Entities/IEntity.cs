using System;

namespace Gwt.Domain.Entities
{
  public interface IEntity
  {
    Guid Id { get; set; }
  }
}