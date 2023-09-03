using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APICatalogo.Models;
using APICatalogo.Service;
using Microsoft.IdentityModel.Tokens;

namespace APICatalogo.Services;

public class TokenService: ITokeService {

  public string GerarToken(string key, string issues, string audience, UserModel user){
    var claims = new []{
      new Claim(ClaimTypes.Name, user.UserName),
      new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
    };

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    
    var token = new JwtSecurityToken(issuer: issues,
                                    audience: audience,
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(120),
                                    signingCredentials: credentials);
    
    var tokenHandler = new JwtSecurityTokenHandler();
    var stringToken = tokenHandler.WriteToken(token);
    
    return stringToken;
  }
}