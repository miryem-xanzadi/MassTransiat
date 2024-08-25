using MassTransit;

namespace MassTransitDemo.Publisher
{
    public class PingPublisher : BackgroundService
    {
        private readonly ILogger<PingPublisher> _logger;
        private readonly IBus _bus;

        public PingPublisher(ILogger<PingPublisher> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Ensuring asynchronous execution
                await Task.Yield();

                // Using Console.KeyAvailable to avoid blocking the loop
                if (Console.KeyAvailable)
                {
                    var keyPressed = Console.ReadKey(true);

                    // Correcting syntax error in the conditional statement
                    if (keyPressed.Key != ConsoleKey.Escape)
                    {
                        _logger.LogInformation($"Pressed {keyPressed.Key}");
                        await _bus.Publish(new Ping 
                        { 
                            Button =keyPressed.Key.ToString()
                        });

                    }
                    else
                    {
                        // Optional: Handle the case where the Escape key is pressed, if needed
                        break; // Exiting the loop if Escape is pressed
                    }
                }

                await Task.Delay(200, stoppingToken); // Passing the stoppingToken to Task.Delay for graceful cancellation
            }
        }
    }
}
