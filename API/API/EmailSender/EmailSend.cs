
using System.Net.Mail;
using System.Net;

namespace API.EmailSender
{
    public class EmailSend
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "labce-bases-gr03-24@outlook.com";
            var pw = "12345Abc!";
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail, to: email, subject, message));
        }
    }
}
