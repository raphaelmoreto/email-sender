using EmailSender.Domain.Entities;

namespace EmailSender.Application.Abstractions
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
    }
}
