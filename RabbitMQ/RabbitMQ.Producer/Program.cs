using RabbitMQ.Client;
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

int i = 1;
while (i<int.MaxValue)
{

    var message = new { Name = "Producer", Message = $"Console Message -> Code Academy", MessageID = i };
    i++;

    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
    channel.BasicPublish("", queue_name, null, body);
    Console.WriteLine($"{i,-10} Numarali mesaj gonderildi ");
}


//docker-compose up -d