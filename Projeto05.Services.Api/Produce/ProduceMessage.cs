using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Projeto05.Services.Api.Models;
using Projeto05.Services.Api.Settings;
using RabbitMQ.Client;
using System.Text;

namespace Projeto05.Services.Api.Produce
{
    public class ProduceMessage
    {
        private readonly ConnectionFactory? _connectionFactory;
        private readonly RabbitMQSettings? _rabbitMQSerrings;

        public ProduceMessage(IOptions<RabbitMQSettings> options)
        {
            _rabbitMQSerrings = options.Value;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitMQSerrings.Host,
                UserName = _rabbitMQSerrings.Username,
                Password = _rabbitMQSerrings.Password,
            };
        }

        //Metodos para publicar  uma mensagem na fila
        public void Publish(MessageViewModel model)
        {
            //conectando no servidor de mensageria
            using (var connection = _connectionFactory?.CreateConnection())
            {
                //criando um objeto na fila de mensagens
                using(var chanel = connection?.CreateModel())
                {
                    //criando um objeto na fila de mensagens
                    chanel?.QueueDeclare(
                        queue: _rabbitMQSerrings?.Queue, //nome da fila
                        durable: true,  //dados permanecerão na fila mesmo após reiniciar o RabbitMQ
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    //escreve o conteudo para gravar a fila
                    var json = JsonConvert.SerializeObject(model);
                    var bytes = Encoding.UTF8.GetBytes(json);

                    //grava na fila
                    chanel?.BasicPublish(
                        exchange: String.Empty,
                        routingKey: _rabbitMQSerrings?.Queue,
                        basicProperties:null,
                        body: bytes
                        );
                }
            }
        }

    }
}
