using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Pagg.SendMailBankSlipAvailable
{
    public static class PaggSendMailBankSlipAvailable
    {
        [FunctionName("PaggSendMailBankSlipAvailable")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "SendMailBankSlipAvailable")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request - Alerta de cobrança.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                var numero = data?.Numero;
                var vencimento = data?.Vencimento;
                var valor = data?.Valor;
                var cedente = data?.Cedente;
                var sacado = data?.Sacado;
                var email = data?.Email;

                // Envia e-mail com as informações do boleto

                return new OkObjectResult($"Alerta de cobrança enviada com sucesso para o cliente: {sacado}-{email}.");
            } catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
