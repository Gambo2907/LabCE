
using System.Net.Mail;
using System.Net;
/*
 * Encargada del envio de mensajes a los destinatarios correspondientes 
 */
namespace API.EmailSender
{
    public class EmailSend
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //Correo y password del e-mail del remitente, se modela como un correo de negocios. 
            var mail = "labce-bases-gr03-24@outlook.com";
            var pw = "12345Abc!";
            /*Crea un nuevo cliente para enviar correos, en este caso el .outlook.com es por el dominio del correo
             * si su correo es de otro dominio este se puede cambiar. 
             */
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
