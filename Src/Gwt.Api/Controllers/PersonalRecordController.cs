using System.Threading.Tasks;
using Gwt.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gwt.Api.Controllers.PersonalRecord
{
  public class PersonalRecordController : BaseController
  {
    private readonly IUserManager _userManager;
    public PersonalRecordController(IUserManager userManager)
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