# 📧 EmailSender - Aplicação de Envio de E-mails

## 📖 Sobre o Projeto

O **EmailSender** é uma aplicação desktop desenvolvida em C# com WPF, cujo objetivo é realizar o envio de e-mails por meio de um servidor SMTP.

A aplicação permite configurar e enviar mensagens contendo:

* 📩 Destinatários principais (Para)
* 📋 Destinatários em cópia (Cc)
* 🔒 Destinatários em cópia oculta (Cco/Bcc)
* 📝 Assunto personalizado
* 💬 Corpo da mensagem

O objetivo do projeto é demonstrar a implementação de uma aplicação desktop utilizando **WPF, MVVM, DDD e Clean Architecture** (também, para aprender a como fazer uma aplicaçãode envios de e-mails KK), mantendo as responsabilidades organizadas e o código desacoplado.

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, utilizando separação de responsabilidades entre as camadas:

```text
EmailSender
│
├── EmailSender.Wpf
│   ├── Views / MainWindow.xaml       → Interface gráfica
│   ├── ViewModels                    → Lógica de apresentação (MVVM)
│   ├── App.xaml                      → Inicialização da aplicação
│   └── App.xaml.cs                   → Composição das dependências
│
├── EmailSender.Application
│   ├── Services                      → Serviços da aplicação
│
├── EmailSender.Domain
│   ├── Abstractions                  → Contratos e interfaces
│   └── Entities                      → Entidades e regras de domínio
│
└── EmailSender.Infrastructure
    ├── Configuration                 → Configurações SMTP
    └── Email                         → Implementação do envio de e-mails
```

### 🔹 Responsabilidade de cada camada

**EmailSender.Domain**
- Contém as entidades e regras fundamentais do domínio.
- Define a estrutura da mensagem por meio da entidade `EmailMessage`.
- Não possui dependências de infraestrutura ou interface gráfica.

**EmailSender.Application**
- Contém os casos de uso da aplicação.
- Realiza validações antes de solicitar o envio do e-mail.
- Define contratos por meio de interfaces, como `IEmailSender`.

**EmailSender.Infrastructure**
- Implementa os recursos externos necessários para a aplicação.
- Realiza a comunicação com o servidor SMTP.
- Utiliza MailKit e MimeKit para construir e enviar mensagens.
- Carrega as configurações de conexão SMTP.

**EmailSender.Wpf**
- Contém a interface gráfica da aplicação.
- Utiliza o padrão MVVM para separar interface e lógica de apresentação.
- Gerencia os dados informados pelo usuário.
- Inicializa e conecta as dependências das demais camadas.

---

## ⚙️ Tecnologias Utilizadas

* **C#** (linguagem de programação)
* **.NET / WPF** (desenvolvimento desktop)
* **XAML** (construção da interface gráfica)
* **MVVM** (padrão de arquitetura para aplicações WPF)
* **Clean Architecture** (separação de responsabilidades)
* **DDD (Domain-Driven Design)** (organização do domínio)
* **MailKit** (comunicação com servidores SMTP)
* **MimeKit** (construção e manipulação de mensagens de e-mail)
* **DotNetEnv** (carregamento de variáveis de ambiente através de arquivo `.env`)
* **INotifyPropertyChanged** (notificação de alterações nas propriedades e atualização do Data Binding)
* **ICommand** (implementação de comandos vinculados aos componentes da interface)

---

## 📦 Dependências por Projeto

### 🔹 EmailSender.Wpf

* WPF / XAML
* DotNetEnv
* ViewModels
* Implementação de comandos (`ICommand`)
* Data Binding
* Referências para Application e Domain

### 🔹 EmailSender.Application

* Casos de uso/Serviço (`SendEmailUseCase`)
* Validação dos dados antes do envio
* Referência para Domain e Infrastructure

### 🔹 EmailSender.Domain

* Interfaces e abstrações
* Entidade `EmailMessage`
* Organização dos destinatários
* Normalização dos endereços de e-mail
* Regras fundamentais do domínio

### 🔹 EmailSender.Infrastructure

* MailKit
* MimeKit
* Configurações SMTP (`SmtpSettings`)
* Implementação de `IEmailSender`
* Comunicação com servidor SMTP
* Referência para Domain

---

## 🔐 Configuração SMTP

A aplicação utiliza um arquivo `.env` para armazenar as configurações necessárias para conexão com o servidor SMTP.

Crie um arquivo chamado `.env` na raiz do projeto `EmailSender.Wpf`:

```env
SMTP_HOST=smtp.gmail.com
SMTP_PORT=587
SMTP_USER=seuemail@gmail.com
SMTP_PASSWORD=SUA_SENHA_DE_APP
SMTP_FROM=seuemail@gmail.com
SMTP_FROM_NAME=EmailSender
```

### Variáveis de configuração

| Variável | Descrição |
|---|---|
| `SMTP_HOST` | Endereço do servidor SMTP |
| `SMTP_PORT` | Porta de comunicação SMTP |
| `SMTP_USER` | Usuário de autenticação |
| `SMTP_PASSWORD` | Senha ou senha de aplicativo |
| `SMTP_FROM` | Endereço remetente |
| `SMTP_FROM_NAME` | Nome exibido como remetente |

**Importante:** o arquivo `.env` deve ser configurado no Visual Studio com:

* Build Action: `Content`
* Copy to Output Directory: `Copy if newer`

O arquivo também deve ser incluído no `.gitignore`, evitando que credenciais sejam enviadas ao repositório.

Para compartilhar a estrutura das configurações, utilize um arquivo `.env.example` sem credenciais reais.

> ⚠️ O arquivo `.env` não criptografa as informações. Como esta é uma aplicação desktop, as credenciais SMTP também não ficam realmente protegidas de quem possui acesso à máquina ou aos arquivos publicados. Para uma aplicação distribuída a outros usuários, o ideal é centralizar o envio em um backend.

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

* .NET SDK compatível com a versão utilizada no projeto
* Visual Studio com suporte ao desenvolvimento desktop com WPF
* Servidor SMTP configurado
* Conta de e-mail com permissão para autenticação SMTP

### Passos

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/EmailSender.git

# Acesse a pasta
cd EmailSender
```

Após clonar o projeto:

1. Abra a solução `EmailSender.sln` no Visual Studio.
2. Configure o arquivo `.env` dentro do projeto `EmailSender.Wpf`.
3. Preencha as credenciais do servidor SMTP.
4. Verifique se o projeto `EmailSender.Wpf` está definido como projeto de inicialização.
5. Restaure os pacotes NuGet.
6. Execute a aplicação.

Também é possível executar pelo terminal:

```bash
dotnet restore

dotnet run --project EmailSender.Wpf
```

---

## 📊 Funcionalidades

* Envio de e-mails via SMTP.
* Definição de múltiplos destinatários.
* Suporte aos campos Para, Cc e Cco.
* Personalização do assunto.
* Personalização do corpo da mensagem.
* Validação dos endereços de e-mail.
* Carregamento de configurações SMTP através de `.env`.
* Execução assíncrona do envio de mensagens.
* Exibição do status do envio na interface.

---

## 🧠 Boas Práticas Aplicadas

* Separação de responsabilidades entre camadas.
* Aplicação dos princípios da Clean Architecture.
* Utilização de conceitos do DDD.
* Aplicação do padrão MVVM.
* Uso de interfaces para desacoplamento.
* Injeção e composição de dependências.
* Encapsulamento das configurações SMTP.
* Utilização de `async/await` para operações assíncronas.
* Validação dos dados antes de executar operações externas.
* Uso de variáveis de ambiente para configurações.
* Separação entre regras de negócio e detalhes de infraestrutura.

---

## 👨‍💻 Autor

Desenvolvido por **Raphael**.
