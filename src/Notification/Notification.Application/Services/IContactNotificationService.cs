namespace myRideApp.Notification.Application.Services;

public interface IContactNotificationService
{
    Task NotifyAsync<TEvent, TContact>(TEvent @event, IEmailTemplate<TEvent, TContact> template) where TContact : IEntity;
}