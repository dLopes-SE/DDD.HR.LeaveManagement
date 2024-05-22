using Application.Interfaces.Email;
using Application.Models.Email;

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