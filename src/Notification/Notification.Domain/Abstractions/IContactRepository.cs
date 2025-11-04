namespace myRideApp.Notification.Domain.Abstractions;

public interface IContactRepository<T> where T : IEntity 
{
    Task<T> GetContactAsync(Guid contactId);
    Task UpdateContactAsync(T contact);
}