using System;
using Gwt.Common;

namespace Gwt.Infrastructure.MachineDateTime
{
  public class CurrentDateTime : IDateTime
  {
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;
  }
}