
namespace EmailSender.Infrastructure.Configuration
{
    public sealed class SmtpSettings
    {
        public string Host { get; init; } = Required("SMTP_HOST");

        public int Port { get; init; } = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out var port) ? port : 587;

        public string UserName { get; init; } = Required("SMTP_USER");

        public string Password { get; init; } = Required("SMTP_PASSWORD");

        public string FromAddress { get; init; } = Required("SMTP_FROM");

        public string FromName { get; init; } = Environment.GetEnvironmentVariable("SMTP_FROM_NAME") ?? "EmailSender";

        private static string Required(string nome)
        {
            var value = Environment.GetEnvironmentVariable(nome);
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException($"A VARIÁVEL DE AMBIENTE {nome} NÃO FOI CONFIGURADA");

            return value;
        }
    }
}
