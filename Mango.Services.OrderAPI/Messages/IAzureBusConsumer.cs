namespace Mango.Services.OrderAPI.Messages
{
    public interface IAzureBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
