namespace Mango.Services.OrderAPI.Messaging
{
    public interface IAzureBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
