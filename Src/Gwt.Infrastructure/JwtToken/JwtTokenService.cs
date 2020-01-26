using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gwt.Application.Common.Interfaces;
using Gwt.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gwt.Infrastructure.JwtToken
{
  public class JwtTokenService : IJwtTokenService
  {
    private readonly IUserManager _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTime _dateTime;
    public JwtTokenService(IUserManager userManager, IDateTime dateTime, IOptions<JwtSettings> jwtSettings)
    {
      _userManager = userManager;
      _dateTime = dateTime;
      _jwtSettings = jwtSettings.Value;
    }
    public async Task<(Guid, string)> GenerateJwtToken(string email)
    {
      var tokenId = Guid.NewGuid();
      var user = await _userManager.FindByEmailAsync(email);
      var tokenHandler = new JwtSecurityTokenHandler();
      var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

      var roles = await _userManager.GetRolesByUserIdAsync(user.Id);
      foreach (var role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = _dateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpireDays));

      var token = new JwtSecurityToken(
          _jwtSettings.Issuer,
          _jwtSettings.Issuer,
          claims,
          expires: expires,
          signingCredentials: creds
      );

      return (tokenId, tokenHandler.WriteToken(token));
    }
  }
}