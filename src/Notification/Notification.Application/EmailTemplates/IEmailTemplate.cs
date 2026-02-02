namespace myRideApp.Notification.Application.EmailTemplates;

public interface IEmailTemplate<TEvent, TContact> where TContact : IEntity
{
    string BuildSubject(TEvent evt, TContact contact);
    string BuildBody(TEvent evt, TContact contact);
}
