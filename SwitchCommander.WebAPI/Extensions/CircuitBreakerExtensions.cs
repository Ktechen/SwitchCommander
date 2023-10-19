using Polly;

namespace SwitchCommander.WebAPI.Extensions;

public static class CircuitBreakerExtensions
{
    public static IServiceCollection AddCircuitBreakerPolicy(this IServiceCollection services, int maxFailures,
        TimeSpan duration)
    {
        var circuitBreaker = Policy
            .Handle<UnauthorizedAccessException>() // Replace with your specific exception type
            .CircuitBreaker(maxFailures, duration);

        services.AddSingleton(circuitBreaker);

        return services;
    }
}