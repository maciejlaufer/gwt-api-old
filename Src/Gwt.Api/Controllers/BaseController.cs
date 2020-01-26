using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gwt.Api.Controllers
{
  [Authorize]
  [ApiController]
  [Route("/api")]
  public class BaseController : Controller
  {

  }
}