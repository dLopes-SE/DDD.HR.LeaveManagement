using Application.Exceptions;
using Application.Interfaces.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;

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

      var jwtSecurityToken = GenerateJwtSecurityToken(user);

      return new AuthResponse
      {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
      };
    }

    public Task<RegistrationResponse> Register(RegistrationRequest request)
    {
      throw new NotImplementedException();
    }

    public JwtSecurityToken GenerateJwtSecurityToken(ApplicationUser user)
    {
      throw new NotImplementedException();
    }
  }
}
