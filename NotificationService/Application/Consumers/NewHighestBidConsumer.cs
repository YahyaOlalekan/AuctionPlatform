using MassTransit;
using NotificationService.Infrastructure.Repositories;
using Shared.Events;

namespace NotificationService.Application.Consumers
{
    public class NewHighestBidConsumer : IConsumer<NewHighestBidEvent>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NewHighestBidEvent> _logger;

        public NewHighestBidConsumer(INotificationRepository notificationRepository, ILogger<NewHighestBidEvent> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<NewHighestBidEvent> context)
        {
            var NewHighestBidEvent = context.Message;

            _logger.LogInformation($"Highest bidder is: {NewHighestBidEvent.UserId}");

           

            await Task.CompletedTask;
        }
    }
}
