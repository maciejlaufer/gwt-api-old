using Gwt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gwt.Application.Common.Interfaces
{
  public interface IGwtDbContext
  {
    DbSet<ApplicationUser> ApplicationUsers { get; set; }
  }
}