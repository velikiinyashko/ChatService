using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ClientChat.Model
{
    class MainModel
    {

    }

    public class ConsumerMessage
    {
        private Dispatcher _disp;

        public ConsumerMessage()
        {
            _disp = Dispatcher.CurrentDispatcher;
        }

        public void Start(ObservableCollection<GetMessage> collection)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                while (true)
                {
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        _disp.Invoke(new Action(() =>
                        {
                            collection.Add(new GetMessage { Message = message });
                        }));
                        
                    };

                    channel.BasicConsume(queue: "test",
                                                            autoAck: true,
                                                            consumer: consumer);
                }
            }
        }
    }

    public class GetMessage
    {
        public string Message { get; set; }
    }
}
