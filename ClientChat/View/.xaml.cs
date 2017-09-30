using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MassTransit;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ClientChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MessModel();
            //Start();
            Rabbit rabbit = new Rabbit("localhost");
            rabbit.Start();
            //Task task = new Task(rabbit.Start);
            //task.Start();
        }

        private void Click_Read_Queue(object sender, RoutedEventArgs e)
        {

        }


        public void Start()
        {

            MessModel txt = new MessModel();
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
                        MessageBox.Show(message);
                        ViewQueue.Text = message;
                    };

                    channel.BasicConsume(queue: "test",
                                        autoAck: true,
                                        consumer: consumer);
                }
            }
        }
    }

    public class TestMessage
    {
        public string Text { get; set; }
    }

    public class Rabbit
    {
        private string Host;

        public Rabbit(string Host)
        {
            this.Host = Host;
        }

        public void Start()
        {

            MessModel txt = new MessModel();
            var factory = new ConnectionFactory()
            {
                HostName = Host
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

                consumer.Received += (model, ea) =>
                            {
                                var body = ea.Body;
                                var message = Encoding.UTF8.GetString(body);
                                txt.Message = message;
                            };

                channel.BasicConsume(queue: "test",
                                    autoAck: true,
                                    consumer: consumer);
            }
        }
    }
}
