using Gwt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gwt.Application.Common.Interfaces
{
  public interface IGwtDbContext
  {
    DbSet<Profile> Profiles { get; set; }
  }
}