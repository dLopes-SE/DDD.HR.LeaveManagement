using BlazorUI.Contracts;
using BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages;

public partial class Login
{
  public RegisterVM Model { get; set; }
  [Inject]
  public NavigationManager NavigationManager { get; set; }
  [Inject]
  public IAuthenticationService AuthenticationService { get; set; }
  public string Message { get; set; }

  public Login()
  {
  }

  protected override void OnInitialized()
  {
    Model = new RegisterVM();
  }

  protected async void HandleValidSubmit()
  {
    if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
      NavigationManager.NavigateTo("/");

    Message = "Wrong username or password. Please try again.";
  }
}