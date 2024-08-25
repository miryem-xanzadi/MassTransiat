using MassTransit;

namespace MassTransitDemo.Consumer
{
    public class PingConsumer : IConsumer<Ping>
    {
        private readonly ILogger<PingConsumer> _logger;

        public PingConsumer(ILogger<PingConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Ping> context)
        {
            var ping = context.Message;

            if (ping != null) 
            {
                var button = context.Message.Button;
                var title = context.Message.Title;
                _logger.LogInformation($"Button pressed {button}, title: {title}");

            }
            
            return Task.CompletedTask;
        }
    }
}
