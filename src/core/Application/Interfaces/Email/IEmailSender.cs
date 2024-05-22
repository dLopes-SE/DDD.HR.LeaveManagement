using Application.Models.Email;

namespace Application.Interfaces.Email
{
    public interface IEmailSender
  {
    Task<bool> SendEmail(EmailMessage email);
  }
}
