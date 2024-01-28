using BankSlipRegistration.Service.Services;
using MassTransit;
using Pagg.Core.Entities;

namespace BankSlipRegistration.Service.Events
{
    public class BankSlipRegistrationConsumer : IConsumer<Pagg.Core.Entities.Boleto>
    {
        private readonly IAlertService _alertService;

        public BankSlipRegistrationConsumer(IAlertService alertService)
        {
            _alertService = alertService;
        }

        public async Task Consume(ConsumeContext<Pagg.Core.Entities.Boleto> context)
        {
            Console.WriteLine($"MessageId: {context.MessageId} /// Message: {context.Message}");

            var boleto = (Pagg.Core.Entities.Boleto)context.Message;

            // Passo 1 - Chama api de registro
            await RegisterCip(boleto);

            // Passo 2 - Chama uma função do azure para enviar um email de cobrança
            await SendMailBankSlipAvailable(boleto.Numero,
                boleto.Vencimento,
                boleto.Valor,
                boleto.Cedente,
                boleto.Sacado,
                boleto.Email);
        }

        private async Task RegisterCip(Boleto boleto)
        {
            Console.WriteLine($"Registrando o boleto n° {boleto.Numero} para o sacado {boleto.Sacado}.");
        }

        private async Task SendMailBankSlipAvailable(int numero,
            DateTime vencimento,
            decimal valor,
            string cedente,
            string sacado,
            string email) => await _alertService.SendMailBankSlipAvailable(numero, vencimento, valor, cedente, sacado, email);
    }
}
