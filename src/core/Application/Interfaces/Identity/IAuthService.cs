using Application.Models.Identity;

namespace Application.Interfaces.Identity
{
  public interface IAuthService
  {
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
  }
}
