namespace BankSlipRegistration.Service.Services
{
    public interface IAlertService
    {
        Task SendMailBankSlipAvailable(int numero,
            DateTime vencimento,
            decimal valor,
            string cedente,
            string sacado,
            string email);
    }
}
