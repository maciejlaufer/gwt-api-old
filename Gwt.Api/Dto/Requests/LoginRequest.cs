using System.ComponentModel.DataAnnotations;

namespace Gwt.Api.Dto.Requests
{
  public class LoginRequest
  {
    [Required]
    public string UsernameOrEmail { get; set; }
    [Required]
    public string Password { get; set; }
  }
}