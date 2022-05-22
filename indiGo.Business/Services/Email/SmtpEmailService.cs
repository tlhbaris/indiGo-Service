using System.Net;
using System.Net.Mail;
using System.Text;
using indiGo.Core.Configuration;
using indiGo.Core.Emails;
using indiGo.Core.Services;
using Microsoft.Extensions.Configuration;

namespace indiGo.Business.Services.Email;

public class SmtpEmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailSettings EmailSettings { get; }

    public SmtpEmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        //this.EmailSettings = .
        var emailSettings = _configuration.GetSection("GmailSettings");
        this.EmailSettings = new EmailSettings
        {
            SenderMail = emailSettings["SenderMail"],
            Password = emailSettings["Password"],
            Smtp = emailSettings["Smtp"],
            SmtpPort = Convert.ToInt32(emailSettings["SmtpPort"])
        };
    }

    public Task SendEmailAsync(MailModel model)
    {
        var mail = new MailMessage() { From = new MailAddress(this.EmailSettings.SenderMail) };

        foreach (var c in model.To)
        {
            mail.To.Add(new MailAddress(c.Email, c.Name));
        }

        foreach (var cc in model.Cc)
        {
            mail.CC.Add(new MailAddress(cc.Email, cc.Name));
        }

        foreach (var bcc in model.Bcc)
        {
            mail.Bcc.Add(new MailAddress(bcc.Email, bcc.Name));
        }

        if (model.Attachs is { Count: > 0 })
        {
            foreach (var attach in model.Attachs)
            {
                var filestream = attach as FileStream;
                var info = new FileInfo(filestream.Name);

                mail.Attachments.Add(new Attachment(attach, info.Name));
            }
        }

        mail.IsBodyHtml = true;
        mail.BodyEncoding = Encoding.UTF8;
        mail.SubjectEncoding = Encoding.UTF8;
        mail.HeadersEncoding = Encoding.UTF8;

        mail.Subject = model.Subject;
        mail.Body = model.Body;

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        var smtpClient = new SmtpClient(this.EmailSettings.Smtp, this.EmailSettings.SmtpPort)
        {
            Credentials = new NetworkCredential(this.EmailSettings.SenderMail, this.EmailSettings.Password),
            EnableSsl = true
        };

        return smtpClient.SendMailAsync(mail);

    }
}