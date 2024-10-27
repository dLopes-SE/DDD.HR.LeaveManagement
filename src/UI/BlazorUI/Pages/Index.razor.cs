using BlazorUI.Contracts;
using BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUI.Pages;

public partial class Index
{
  [Inject]
  private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
  [Inject]
  public NavigationManager NavigationManager { get; set; }

  [Inject]
  public IAuthenticationService AuthenticationService { get; set; }

  protected void CreateLeaveType()
  {
    NavigationManager.NavigateTo("/leavetypes/create/");
  }

  protected override async Task OnInitializedAsync()
  {
    await ((ApiAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
  }

  protected void GoToLogin()
  {
    NavigationManager.NavigateTo("login");
  }

  protected void GoToRegister()
  {
    NavigationManager.NavigateTo("register/");
  }

  protected async Task LogoutAsync()
  {
    await AuthenticationService.Logout();
  }
}