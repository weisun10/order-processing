namespace OrderProcessing.Application.Interfaces
{
    public interface IMessagePublisher
    {
        Task Publish<T>(T message);
    }
}
