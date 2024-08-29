using Microsoft.AspNetCore.Connections;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace RoomService.Application.Services
{
    public class AuctionRoomEventPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public AuctionRoomEventPublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "AuctionExchange", type: ExchangeType.Direct);
        }

        public void PublishAuctionStarted(Guid auctionRoomId)
        {
            var message = JsonSerializer.Serialize(new { AuctionRoomId = auctionRoomId });
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "AuctionExchange", routingKey: "AuctionStarted", basicProperties: null, body: body);
        }

        public void PublishAuctionEnded(Guid auctionRoomId)
        {
            var message = JsonSerializer.Serialize(new { AuctionRoomId = auctionRoomId });
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "AuctionExchange", routingKey: "AuctionEnded", basicProperties: null, body: body);
        }
    }

}
