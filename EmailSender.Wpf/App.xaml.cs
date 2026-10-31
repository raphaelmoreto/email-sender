using DotNetEnv;
using EmailSender.Domain.Abstractions;
using EmailSender.Application.Services;
using EmailSender.Infrastructure.Configuration;
using EmailSender.Infrastructure.Email;
using System.Windows;
using System.IO;

namespace EmailSender.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var envPath = Path.Combine(AppContext.BaseDirectory, ".env");
            Env.Load(envPath);

            base.OnStartup(e);

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public SendEmailService CreateSendEmailService()
        {
            var settings = new SmtpSettings();
            IEmailSender sender = new MailKitEmailSender(settings);

            return new SendEmailService(sender);
        }
    }

}
