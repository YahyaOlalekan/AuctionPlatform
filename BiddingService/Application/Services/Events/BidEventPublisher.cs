using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using BiddingService.Application.Services;

namespace BiddingService.Application.Services.Events
{
    public class BidEventPublisher : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public BidEventPublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "BiddingExchange", type: ExchangeType.Direct);
        }

        // Method to publish using an anonymous object
        public void PublishNewHighestBid(Guid auctionRoomId, decimal amount)
        {
            var message = JsonSerializer.Serialize(new { AuctionRoomId = auctionRoomId, Amount = amount });
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "BiddingExchange",
                                  routingKey: "NewHighestBid",
                                  basicProperties: null,
                                  body: body);
        }

        // Method to publish using a strongly-typed event
        public void PublishNewHighestBid(NewHighestBidEvent newHighestBidEvent)
        {
            var message = JsonSerializer.Serialize(newHighestBidEvent);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "BiddingExchange",
                                  routingKey: "NewHighestBid",
                                  basicProperties: null,
                                  body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }


}
