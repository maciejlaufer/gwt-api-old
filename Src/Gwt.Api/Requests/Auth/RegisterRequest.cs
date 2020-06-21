using System.ComponentModel.DataAnnotations;

namespace Gwt.Api.Requests.Auth
{
  public class RegisterRequest
  {
    [Required]
    public string Email { get; set; }
    [Required]
    public string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string RepeatPassword { get; set; }
  }
}