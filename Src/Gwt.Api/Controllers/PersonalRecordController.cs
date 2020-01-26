using System.Threading.Tasks;
using Gwt.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gwt.Api.Controllers.PersonalRecord
{
  public class PersonalRecordController : BaseController
  {
    private readonly IUserManager _userManager;
    private readonly ICurrentUserService _currentUserService;
    public PersonalRecordController(IUserManager userManager, ICurrentUserService currentUserService)
    {
      _userManager = userManager;
      _currentUserService = currentUserService;
    }

    [Route("personal-records")]
    public async Task<IActionResult> Init()
    {
      var test = _currentUserService.UserId;
      return Ok("Hello world!");
    }
  }
}