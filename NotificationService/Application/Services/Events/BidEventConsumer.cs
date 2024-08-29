using NotificationService.Application.UnitOfWork;
using NotificationService.Infrastructure.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationService.Application.Services.Events
{
    public class BidEventConsumer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BidEventConsumer(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "BiddingExchange", type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: "NotificationQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "NotificationQueue", exchange: "BiddingExchange", routingKey: "NewHighestBid");
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var newHighestBidEvent = JsonSerializer.Deserialize<NewHighestBidEvent>(message);

                // Handle the event (e.g., send notifications)
                //var notification = new Notification(newHighestBidEvent.UserId, $"New highest bid: {newHighestBidEvent.Amount}");
                //await _notificationRepository.AddAsync(notification);
                //await _unitOfWork.SaveChangesAsync();
            };

            _channel.BasicConsume(queue: "NotificationQueue", autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }

}
