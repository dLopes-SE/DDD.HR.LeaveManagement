using Application.Interfaces.Identity;
using Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;
  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("login")]
  public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
  {
    return Ok(await _authService.Login(request));
  }

  [HttpPost("register")]
  public async Task<ActionResult<AuthResponse>> Register(RegistrationRequest request)
  {
    return Ok(await _authService.Register(request));
  }

}
