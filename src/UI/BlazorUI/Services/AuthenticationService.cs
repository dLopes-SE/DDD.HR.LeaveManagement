using Blazored.LocalStorage;
using BlazorUI.Contracts;
using BlazorUI.Services.Base;

namespace BlazorUI.Services;
public class AuthenticationService : BaseHttpService, IAuthenticationService
{
  public AuthenticationService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
  {
  }

  public async Task<bool> AuthenticateAsync(string email, string password)
  {
    try
    {
      var authRequest = new AuthRequest()
      {
        Email = email,
        Password = password
      };

      var authResponse = await _client.LoginAsync(authRequest);

      if (!string.IsNullOrEmpty(authResponse.Token))
      {
        await _localStorage.SetItemAsStringAsync("token", authResponse.Token);

        // Set claims in Blazor and login state
        return true;
      }

      return false;
    }
    catch
    {
      return false;
    }
  }

  public async Task Logout()
  {
    await _localStorage.RemoveItemAsync("token");

    // remove claims in Blazor and invalidate login state
  }

  public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
  {
    try
    {
      var registrationRequest = new RegistrationRequest()
      {
        FirstName = firstName,
        LastName = lastName,
        UserName = userName,
        Email = email,
        Password = password
      };

      var authResponse = await _client.RegisterAsync(registrationRequest);
      if (!string.IsNullOrEmpty(authResponse.Id))
      {
        return true;
      }

      return false;
    }
    catch
    {
      return false;
    }
  }
}
