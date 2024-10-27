using Blazored.LocalStorage;
using BlazorUI.Contracts;
using BlazorUI.Providers;
using BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUI.Services;
public class AuthenticationService : BaseHttpService, IAuthenticationService
{
  private readonly AuthenticationStateProvider _authenticationStateProvider;

  public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
  {
    _authenticationStateProvider = authenticationStateProvider;
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
        await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

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
    // Remove claims and invalidate login state
    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
  }

  public async Task<(bool, string)> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
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
        return (true, string.Empty);
      }

      return (false, String.Empty);
    }
    catch (Exception ex)
    {
      return (false, ex.Message);
    }
  }
}
