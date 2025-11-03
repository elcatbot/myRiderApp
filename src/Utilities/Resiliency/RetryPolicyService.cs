namespace myRideApp.Utilities.Resiliency;

public class RetryPolicyService(ILogger<RetryPolicyService> Logger) 
    : IRetryPolicyService
{
    public async Task ExecuteWithRetryAsync<TEvent>(Func<Task> handler)
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (ex, delay, attempt, context) =>
                {
                    Logger.LogWarning(ex, $"Retry {attempt} for {typeof(TEvent).Name}");
                });
        await retryPolicy.ExecuteAsync(handler);
    }
}
