namespace myRideApp.Utilities.Resiliency;

public interface IRetryPolicyService
{
    Task ExecuteWithRetryAsync<TEvent>(Func<Task> handler);
}
