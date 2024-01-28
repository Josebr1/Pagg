using MassTransit;

namespace BankSlipRegistration.Service.Events
{
    public class BankSlipRegistrationConsumer : IConsumer<Pagg.Core.Entities.Boleto>
    {
        public Task Consume(ConsumeContext<Pagg.Core.Entities.Boleto> context)
        {
            Console.WriteLine($"MessageId: {context.MessageId} /// Message: {context.Message}");

            // Chama api de registro

            // Chama function para enviar aleta de boleto disponivel para o pagador


            return Task.CompletedTask;
        }
    }
}
