using EmailSender.Application.Services;
using EmailSender.Domain.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmailSender.Wpf.ViewModels
{
    public class EmailViewModel : INotifyPropertyChanged
    {
        private readonly SendEmailService sendEmailService;

        private string _to = "";
        private string _cc = "";
        private string _bcc = "";
        private string _subject = "";
        private string _body = "";
        private string _status = "";

        public EmailViewModel(SendEmailService sendEmailService)
        {
            this.sendEmailService = sendEmailService;
            EnviarComando = new AsyncRelayCommand(SendAsync);
        }

        public string To
        {
            get => _to;
            set => SetCampo(ref _to, value);
        }

        public string Cc
        {
            get => _cc;
            set => SetCampo(ref _cc, value);
        }

        public string Bcc
        {
            get => _bcc;
            set => SetCampo(ref _bcc, value);
        }

        public string Subject
        {
            get => _subject;
            set => SetCampo(ref _subject, value);
        }

        public string Body
        {
            get => _body;
            set => SetCampo(ref _body, value);
        }

        public string Status
        {
            get => _status;
            set => SetCampo(ref _status, value);
        }

        // "ICommand" É A INTERFACE PADRÃO USADA PARA LIGAR AÇÕES DA INTERFACE GRÁFICA (COMO UM CLIQUE DE UM BOTÃO) DIRETAMENTE A MÉTODOS DA CLASSE ViewModel
        public ICommand EnviarComando { get; }

        private async Task SendAsync()
        {
            try
            {
                Status = "Enviando...";

                var message = new EmailMessage(Subject, Body, ParseEnderecos(To), ParseEnderecos(Cc), ParseEnderecos(Bcc));

                await sendEmailService.ExecuteAsync(message);

                Status = "Mensagem aceita pelo servidor SMTP.";
            }
            catch (Exception ex)
            {
                Status = $"Não foi possível enviar! {ex.Message}";
            }
        }

        private static IEnumerable<string> ParseEnderecos(string value)
        {
            // "StringSplitOptions.RemoveEmptyEntries" REMOVE ENTRADAS VAZIAS DO RESULTADO. POR EXEMPLO, SE HOUVER DUAS VÍRGULAS SEGUIDAS(", ,") ELE IGNORA O ESPAÇO VAZIO GERADO ENTRE ELAS

            // "StringSplitOptions.TrimEntries" REMOVE OS ESPAÇOS EM BRANCO QUE COSTUMAM SOBRAR NAS PONTAS DE CADA ITEM EXTRAÍDO
            return value.Split
            (
                [',', ';'],
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            );
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // "[CallerMemberName] string? nomePropriedade = null" CAPTURA AUTOMÁTICAMENTE O NOME DA PROPRIEDADE QUE CHAMOU O MÉTODO, EVITANDO QUE PRECISE DIGITAR O NOME DELA COMO STRING (EX: "Nome")
        private bool SetCampo<T>(ref T campo, T value, [CallerMemberName] string? nomePropriedade = null)
        {
            if (EqualityComparer<T>.Default.Equals(campo, value))
                return false;

            campo = value;

            // DISPARA O EVENTO "PropertyChanged" AVISANDO A INTERFACE DE USUÁRIO QUE AQUELA PROPRIEDADE ESPECÍFICA FOI MODIFICADA
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));

            return true;
        }
    }

    // O "AsyncRelayCommand" IMPEDE UMA SEGUNDA EXECUÇÃO SIMULTÂNEA DO COMANDO ENQUANTO A PRIMEIRA ESTIVER EM ANDAMENTO
    public sealed class AsyncRelayCommand(Func<Task> execute) : ICommand
    {
        private bool _isRunning;

        public bool CanExecute(object? parameter) => !_isRunning;

        public event EventHandler? CanExecuteChanged;

        public async void Execute(object? parameter)
        {
            if (_isRunning)
                return;

            _isRunning = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            try
            {
                await execute();
            }
            finally
            {
                _isRunning = false;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
