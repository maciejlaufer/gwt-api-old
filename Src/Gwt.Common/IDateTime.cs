﻿using System;

namespace Gwt.Common
{
  public interface IDateTime
  {
    DateTime UtcNow { get; }
    DateTime Now { get; }
  }
}
