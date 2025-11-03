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

        var circuitBreakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (ex, breakTime) => Logger.LogWarning($"Circuit opened for {typeof(TEvent).Name}: {ex.Message}"),
                onReset: () => Logger.LogInformation($"Circuit reset for {typeof(TEvent).Name}"),
                onHalfOpen: () => Logger.LogInformation($"Circuit half-open for {typeof(TEvent).Name}")
            );

        var policy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

        await policy.ExecuteAsync(handler);
    }
}
