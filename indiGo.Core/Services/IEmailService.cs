using indiGo.Core.Emails;

namespace indiGo.Core.Services;

public interface IEmailService
{
    Task SendEmailAsync(MailModel model);
}