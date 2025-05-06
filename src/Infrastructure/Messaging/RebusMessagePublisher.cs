using OrderProcessing.Application.Interfaces;
using Rebus.Bus;

namespace OrderProcessing.Infrastructure.Messaging
{
    public class RebusMessagePublisher : IMessagePublisher
    {
        private readonly IBus _bus;

        public RebusMessagePublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish<T>(T message)
        {
            // TODO: check why target queue is bound with RebusDirect exchange
            await _bus.Send(message, new Dictionary<string, string>());
        }
    }
}
