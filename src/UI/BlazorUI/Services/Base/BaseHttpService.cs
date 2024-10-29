using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BlazorUI.Services.Base;
public class BaseHttpService
{
  protected IClient _client { get; }
  protected readonly ILocalStorageService _localStorage;

  public BaseHttpService(IClient client, ILocalStorageService localStorage)
  {
    _client = client;
    _localStorage = localStorage;
  }

  protected Response<T> ConvertApiExceptions<T>(ApiException ex)
  {
    return ex.StatusCode switch
    {
      400 => new Response<T>
      {
        Message = "Invalid data was submitted",
        ValidationErrors = ex.Response,
        Success = false
      },
      404 => new Response<T>
      {
        Message = "The record was not found",
        Success = false
      },
      204 => new Response<T>
      {
        Message = "No data",
        Success = false
      },
      _ => new Response<T>
      {
        Message = ex.Message,//"Something went wrong, please try again later",
        Success = false
      }
    };
  }

  protected async Task AddBearerToken()
  {
    if (await _localStorage.ContainKeyAsync("token"))
      _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsStringAsync("token"));
  }
}  
