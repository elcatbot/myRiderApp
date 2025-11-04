namespace myRideApp.Notification.Infrastructure;

public class ContactRepository<T>(ICacheService Cache)
    : IContactRepository<T> where T : IEntity
{
    public Task<T> GetContactAsync(Guid contactId)
        => Cache.GetAsync<T>($"{nameof(T)}_{contactId}")!;

    public async Task UpdateContactAsync(T contact)
         => await Cache.SetAsync($"{nameof(T)}_{contact.Id}", contact);
}