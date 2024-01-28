using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankSlipRegistration.Service.Services
{
    public class AlertService : IAlertService
    {
        private readonly IConfiguration _configuration;

        public AlertService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailBankSlipAvailable(int numero, DateTime vencimento, decimal valor, string cedente, string sacado, string email)
        {
            var urlFuncAzure = _configuration.GetSection("AzureFunctions")["Host"];

            using (HttpClient client = new())
            {
                var requestBody = new { Numero = numero,
                    Vencimento = vencimento,
                    Valor = valor,
                    Cedente = cedente,
                    Sacado = sacado,
                    Email = email };

                var json = JsonSerializer.Serialize(requestBody);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                content.Headers.Add("x-functions-key",
                    _configuration.GetSection("AzureFunctions")["Key"]);

                HttpResponseMessage response = await client.PostAsync($"{urlFuncAzure}SendMailBankSlipAvailable", content);

                if(!response.IsSuccessStatusCode)
                {
                    // TODO - Ação para caso o alerta de cobrança não aconteça 
                }
            }
        }
    }
}
