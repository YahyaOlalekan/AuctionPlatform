
using Microsoft.AspNetCore.Mvc;
using NotificationService.Infrastructure.Repositories;

namespace NotificationService.API.Controllers
{
    [ApiController]
    [Route("api/v1/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(Guid userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            if (notifications == null || !notifications.Any())
                return NotFound("No notifications found for this user.");

            return Ok(notifications);
        }
    }

}
