using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Projeto05.Services.Api.Contexts.Entities;
using Projeto05.Services.Api.Helpers;
using Projeto05.Services.Api.Models;
using Projeto05.Services.Api.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Projeto05.Services.Api.Consumer
{
    public class EmailServiceConsumer : BackgroundService
    {
        private readonly RabbitMQSettings? _rabbitMQSerrings;
        private readonly IConnection? _connection;
        private readonly IModel? _model;
        private readonly IServiceProvider? _seviceProvider;
        private readonly EmailHelper _emailHelper;
        private readonly LogHelper _logHelper;
        public EmailServiceConsumer(IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider, EmailHelper emailHelper, LogHelper logHelper)
        {
            _rabbitMQSerrings = options.Value;
            _seviceProvider = serviceProvider;
            _emailHelper = emailHelper;
            _logHelper = logHelper;

            var connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitMQSerrings.Host,
                UserName = _rabbitMQSerrings.Username,
                Password = _rabbitMQSerrings.Password,
            };
            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(
                queue: _rabbitMQSerrings.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_model);

            consumer.Received += (sender, args) =>
            {
                var contentArray = args.Body.ToArray(); 
                var contentString = Encoding.UTF8.GetString(contentArray);

                var message = JsonConvert.DeserializeObject<MessageViewModel>(contentString);

                using (var scope = _seviceProvider.CreateScope())
                {
                    if ("PosUsuarios".Equals(message.From) && "EmailService".Equals(message.To))
                    {
                        var usuario = JsonConvert.DeserializeObject<Usuario>(message.Content);

                        SedMail(usuario);

                        _model.BasicAck(args.DeliveryTag, false);
                    }
                }
            };
            _model.BasicConsume(_rabbitMQSerrings.Queue, false, consumer);
            return Task.CompletedTask;
        }
        private void SedMail(Usuario usuario)
        {
            var emailTo = usuario.Email;
            var subject = $"Confirmação de cadastro de usuário. Id: {usuario.Id}";
            var body = $@"
                Olá {usuario.Nome}, <br/><br/>
                <strong>Parabéns, sua conta de usuário foi cadastrada com sucesso!</strong><br/><br/>
                 ID do Usuário: <strong>{usuario.Id}</strong><br/>
                 Nome: <strong>{usuario.Nome}</strong><br/>
                 CPF: <strong>{usuario.Cpf}</strong><br/>
                 Email: <strong>{usuario.Email}</strong><br/>
                 Att,<br/>
                Mário Duarte             
            ";
            try
            {
                _emailHelper.Send(emailTo, subject, body);
                _logHelper.Create($"Email enviado com sucesso: {JsonConvert.SerializeObject(usuario)}.",LogType.INFO);
            }
            catch (Exception)
            {
                _logHelper.Create($"Erro ao enviar o email: {JsonConvert.SerializeObject(usuario)}.", LogType.ERROR);
            }
                
        }
    }
}
