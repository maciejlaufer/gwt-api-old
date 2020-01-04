using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gwt.Controllers
{
  public class PersonalRecordController : Controller
  {
    [HttpGet("/api")]
    [AllowAnonymous]
    public async Task<IActionResult> Init()
    {
      return Ok("Hello world!");
    }
  }
}