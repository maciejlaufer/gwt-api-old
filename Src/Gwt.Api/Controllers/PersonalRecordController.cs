using System.Threading.Tasks;
using Gwt.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gwt.Api.Controllers.PersonalRecord
{
  public class PersonalRecordController : BaseController
  {
    private readonly UserManager<ApplicationUser> _userManager;
    public PersonalRecordController(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }
    [Route("personal-records")]
    public async Task<IActionResult> Init()
    {
      return Ok("Hello world!");
    }
  }
}