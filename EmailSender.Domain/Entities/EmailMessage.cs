
namespace EmailSender.Domain.Entities
{
    // "sealed" É USADO QUANDO NÃO QUEREMOS QUE A CLASSE SEJA HERDADA. USADO PARA GARANTIR QUE O PAPEL DA CLASSE FIQUE FOCADO SOMENTE NA PRÓPRIA CLASSE
    public sealed class EmailMessage
    {
        public string Subject { get; } // ASSUNTO

        public string Body { get; }

        public IReadOnlyList<string> To { get; }

        public IReadOnlyList<string> Cc { get; }

        public IReadOnlyList<string> Bcc { get; } // CÓPIA OCULTA

        public EmailMessage(string subject, string body, IEnumerable<string> to, IEnumerable<string>? cc = null, IEnumerable<string>? bcc = null)
        {
            Subject = subject.Trim() ?? string.Empty;
            Body = body.Trim() ?? string.Empty;

            To = Normalize(to);
            Cc = Normalize(cc ?? []);
            Bcc = Normalize(bcc ?? []);
        }

        private static IReadOnlyList<string> Normalize(IEnumerable<string> enderecos)
        {
            return enderecos
                .Where(e => !string.IsNullOrWhiteSpace(e)) // REMOVE VALORES VÁZIOS/NULOS
                .Select(e => e.Trim()) // REMOVE O ESPAÇAMENTO NO COMEÇO E NO FINAL DE CADA STRING
                .Distinct(StringComparer.OrdinalIgnoreCase) // REMOVE ITENS DUPLICADOS
                .ToArray(); // CONVERTE O RESULTADO DE VOLTA PARA UM ARRAY DE STRINGS
        }
    }
}
