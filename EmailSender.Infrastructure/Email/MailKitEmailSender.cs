using EmailSender.Domain.Abstractions;
using EmailSender.Domain.Entities;
using EmailSender.Infrastructure.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
//using System.Net.Mail;

namespace EmailSender.Infrastructure.Email
{
    public class MailKitEmailSender(SmtpSettings settings) : IEmailSender
    {
        public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            var mimeMessage = new MimeMessage(); // CRIA UM OBJETO VÁZIO QUE REPRESENTA O E-MAIL NO FORMATO (MIME)

            // ADICIONA O REMETENTE NO E-MAIL
            mimeMessage.From.Add(new MailboxAddress(settings.FromName, settings.FromAddress));

            AddRecipients(mimeMessage.To, message.To);
            AddRecipients(mimeMessage.Cc, message.Cc);
            AddRecipients(mimeMessage.Bcc, message.Bcc);

            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart("plain") { Text = message.Body };

            // CRIA O CLIENTE SMTP ONDE É ENTREGUE A MENSAGEM PRONTA E ELE SE ENCARREGA DE ENVIÁ-LA ATRÁVES DE UM SERVIDOR DE E-MAIL
            using var client = new SmtpClient();

            var socketOption = settings.Port == 465
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.StartTls;

            await client.ConnectAsync(settings.Host, settings.Port, socketOption, cancellationToken);
            await client.AuthenticateAsync(settings.UserName, settings.Password, cancellationToken);
            await client.SendAsync(mimeMessage, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }

        private static void AddRecipients(InternetAddressList destination, IEnumerable<string> addresses)
        {
            foreach (var address in addresses)
            {
                destination.Add(MailboxAddress.Parse(address));
            }
        }
    }
}
