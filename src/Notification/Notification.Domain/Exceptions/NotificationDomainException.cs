namespace myRideApp.Notification.Domain.Exceptions;

public class NotificationDomainException : Exception
{
    public NotificationDomainException() { }
    public NotificationDomainException(string message) : base(message) { }
    public NotificationDomainException(string message, Exception innerException) : base(message, innerException) { }
}