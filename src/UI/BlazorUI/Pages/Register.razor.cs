using BlazorUI.Contracts;
using BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages;

public partial class Register
{
  public RegisterVM Model { get; set; }
  [Inject]
  public NavigationManager NavigationManager { get; set; }
  [Inject]
  public IAuthenticationService AuthenticationService { get; set; }
  public string Message { get; set; }

  protected override void OnInitialized()
  {
    Model = new RegisterVM();
  }

  protected async void HandleRegister()
  {
    try
    {
      var (result, errorMsg) = await AuthenticationService.RegisterAsync(Model.FirstName, Model.LastName, Model.UserName, Model.Email, Model.Password);
      if (result)
        NavigationManager.NavigateTo("/");

      Message = errorMsg;
    }
    catch (Exception ex)
    {
      Message = ex.Message;
    }
  }
}