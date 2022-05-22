namespace indiGo.Core.Emails;

public class MailModel
{
    public List<EmailModel> To { get; set; } = new List<EmailModel>();
    public List<EmailModel> Cc { get; set; } = new List<EmailModel>();
    public List<EmailModel> Bcc { get; set; } = new List<EmailModel>();
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<Stream> Attachs { get; set; } = new List<Stream>();
}