using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SwitchCommander.Persistence.Services.BackgroundTasks;

public class IntervalHostedService<T> : BackgroundService
{
    private readonly ILogger<IntervalHostedService<T>> _logger;
    private readonly int _interval;
    private readonly Action _action;
    
    public IntervalHostedService(
        ILogger<IntervalHostedService<T>> logger, 
        int interval, 
        Action action)
    {
        _logger = logger;
        _interval = interval;
        _action = action;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _action?.Invoke();
            _logger.LogInformation($"{_action?.Method.Name} is executing with interval {_interval}");
            await Task.Delay(_interval, stoppingToken);
        }
    }
}