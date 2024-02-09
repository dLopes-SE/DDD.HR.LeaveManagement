using Application.Interfaces.Email;
using Application.Models;

namespace Infrastructure.EmailService
{
  public class EmailSender : IEmailSender
  {
    public Task<bool> SendEmail(EmailMessage email)
    {
      // Not implemeneted
      return Task.FromResult(true);
    }
  }
}