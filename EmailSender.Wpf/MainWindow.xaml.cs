using System.Windows;
using EmailSender.Wpf.ViewModels;

namespace EmailSender.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var useApplication = ((App)System.Windows.Application.Current).CreateSendEmailService();

            DataContext = new EmailViewModel(useApplication); // QUAL CLASSE OFERECE OS DADOS PARA O BINDING
        }
    }
}