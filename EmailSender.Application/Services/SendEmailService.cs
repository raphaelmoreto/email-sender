using EmailSender.Domain.Abstractions;
using EmailSender.Domain.Entities;

// CONJUNTO DE CLASSES NATIVO DO .NET. USADO PARA ENVIAR E-MAILS ATRAVÉS DE UM SERVIDOR SMTP
using System.Net.Mail;

namespace EmailSender.Application.Services
{
    public sealed class SendEmailService(IEmailSender emailSender)
    {
        // O "(IEmailSender emailSender)" SUBSTITUI O CÓDIGO FEITO ABAIXO
        //private readonly IEmailSender emailSender;

        //public SendEmailService(IEmailSender emailSender)
        //{
        //    this.emailSender = emailSender;
        //}

        // O "CancellationToken" EVITA QUE O SERVIDOR CONTINUE GASTANDO PROCESSAMENTO
        public async Task ExecuteAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(message);

            if (string.IsNullOrWhiteSpace(message.Subject))
                throw new ArgumentException("INFORME O ASSUNTO DO E-MAIL");

            if (string.IsNullOrWhiteSpace(message.Body))
                throw new ArgumentException("INFORME O CORPO DO E-MAIL");

            if (message.To.Count == 0)
                throw new ArgumentException("INFORME AO MENOS UM DESTINATÁRIO NO CAMPO 'Para'");

            ValidateAddresses(message.To, "Para");
            ValidateAddresses(message.Cc, "Cc");
            ValidateAddresses(message.Bcc, "Bcc");

            await emailSender.SendAsync(message, cancellationToken);
        }

        private static void ValidateAddresses(IEnumerable<string> enderecos, string campo)
        {
            foreach (var endereco in enderecos)
            {
                try { _ = new MailAddress(endereco); }
                catch (FormatException) { throw new ArgumentException($"O ENDEREÇO '{endereco}' NO CAMPO {campo} NÃO É VÁLIDO!"); }
            }
        }
    }
}
