namespace Mango.Services.PaymentAPI.Messaging
{
    public interface IAzureBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
