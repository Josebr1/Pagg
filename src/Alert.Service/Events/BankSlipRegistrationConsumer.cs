using MassTransit;

namespace Alert.Service.Events
{
    public class BankSlipRegistrationConsumer : IConsumer<Pagg.Core.Entities.Boleto>
    {
        public Task Consume(ConsumeContext<Pagg.Core.Entities.Boleto> context)
        {
            Console.WriteLine($"MessageId: {context.MessageId} /// Message: {context.Message}");

            return Task.CompletedTask;
        }
    }
}
