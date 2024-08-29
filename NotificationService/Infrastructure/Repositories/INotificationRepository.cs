using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<List<Notification>> GetByUserIdAsync(Guid userId);
    }

}
