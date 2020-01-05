using System.Threading.Tasks;
using Gwt.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gwt.Controllers.PersonalRecord
{
  public class PersonalRecordController : BaseController
  {
    [Route("personal-records")]
    public async Task<IActionResult> Init()
    {

      return Ok("Hello world!");
    }
  }
}