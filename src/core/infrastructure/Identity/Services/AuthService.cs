using Application.Exceptions;
using Application.Interfaces.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services
{
  public class AuthService : IAuthService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user is null)
      {
        throw new NotFoundException($"User with {request.Email} not found.", request.Email);
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
      if (!result.Succeeded)
      {
        throw new BadRequestException($"Credentials for '{request.Email}' aren't valid.");
      }

      var jwtSecurityToken = await GenerateJwtSecurityToken(user);

      return new AuthResponse
      {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
      };
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
      var user = new ApplicationUser()
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        UserName = request.UserName,
        EmailConfirmed = true
      };

      var result = await _userManager.CreateAsync(user, request.Password);

      if (result.Succeeded)
      {
        await _userManager.AddToRoleAsync(user, "Employee");
        return new RegistrationResponse { UserId =  user.Id };
      }

      var str = new StringBuilder();
      foreach (var err in result.Errors)
      {
        str.AppendFormat(".{0}\n", err.Description);
      }

      throw new BadRequestException($"{str}");
    }

    public async Task<JwtSecurityToken> GenerateJwtSecurityToken(ApplicationUser user)
    {
      var userClaims = await _userManager.GetClaimsAsync(user);
      var roles = await _userManager.GetRolesAsync(user);

      var userRoles = roles.Select(r => new Claim(ClaimTypes.Role, r));

      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)
      }
      .Union(userClaims)
      .Union(userRoles);

      var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

      var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

      return new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Audience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
        signingCredentials: signingCredentials);
    }
  }
}
