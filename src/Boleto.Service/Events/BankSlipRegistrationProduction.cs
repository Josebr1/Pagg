using MassTransit;

namespace Boleto.Api.Events
{
    public class BankSlipRegistrationProduction(IBus bus)
    {
        private readonly IBus _bus = bus;

        public async Task Send(string queue, object value)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{queue}"));
            await endpoint.Send(value);
        }
    }
}
