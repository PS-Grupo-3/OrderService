using Application.Features.Order.Commands;
using MediatR;

namespace OrderService.BackgroundServices
{
    public class OrderExpiration : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public OrderExpiration(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    Console.WriteLine($"[{DateTime.UtcNow}] Ejecutando limpieza de órdenes expiradas...");
                    await mediator.Send(new ExpiredOrderCommand(), stoppingToken);
                    Console.WriteLine($"[{DateTime.UtcNow}] Limpieza completada.");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}