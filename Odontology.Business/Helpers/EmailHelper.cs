using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Odontology.Business.Helpers
{
    public static class EmailHelper
    {
        public static void Send(string toEmail, string subject, string text)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("dentmedic.vilnius", "dentmedic.vilnius@gmail.com"));
            mailMessage.To.Add(new MailboxAddress(toEmail.Split("@")[0], toEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = text
            };
            
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtpClient.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                smtpClient.Authenticate("dentmedic.vilnius", "SfPzUnbXvdjS6pK");
                var response = smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}