using EmailSender.Domain.Entities;

namespace EmailSender.Domain.Abstractions
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
    }
}
