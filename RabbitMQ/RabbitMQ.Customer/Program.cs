using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;



const string queue_name = "code-queue";

var factory = new ConnectionFactory
{
    HostName = "localhost",
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: queue_name,
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null

    );

var consumer=new EventingBasicConsumer(channel);

consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine(message);
};

channel.BasicConsume(queue_name, true, consumer);
Console.WriteLine("Started");
Console.ReadLine();

