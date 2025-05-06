using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Domain.Events;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace OrderProcessing.Infrastructure.Messaging
{
    public static class RebusConfigExtensions
    {
        public static IServiceCollection AddRebusWithRabbitMq(this IServiceCollection services, IConfiguration configuration, bool sendOnly = false)
        {
            var rabbitMqSection = configuration.GetSection("RabbitMq");
            string rabbitMqConnectionString = rabbitMqSection.GetValue<string>("ConnectionString") ?? "";
            string queueName = rabbitMqSection.GetValue<string>("QueueName") ?? "";

            if (!sendOnly)
            {
                services.AddRebus(configure => configure
                    .Transport(t => t.UseRabbitMq(rabbitMqConnectionString, queueName))
                    .Routing(r => r.TypeBased()
                    .Map<OrderCreatedEvent>(queueName)
                    ));
            }
            else
            {
                services.AddRebus(configure => configure
                    .Transport(t => t.UseRabbitMqAsOneWayClient(rabbitMqConnectionString))
                    .Routing(r => r.TypeBased()
                    .Map<OrderCreatedEvent>(queueName)
                    ));
            }

            return services;
        }
    }
}
