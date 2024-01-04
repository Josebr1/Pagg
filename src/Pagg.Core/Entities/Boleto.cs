using Pagg.Core.Extensions;

namespace Pagg.Core.Entities
{
    public class Boleto
    {
        public int Numero { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
        public string Cedente { get; set; }
        public string Sacado { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string CodigoDeBarras { get; set; }
        public string LinhaDigitavel { get; set; }
        public string Instrucoes { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
        public decimal Desconto { get; set; }


        public Boleto()
        {
            Numero = Guid.NewGuid().GetHashCode();
            Vencimento = DateTime.Now.AddDays(15);
            Valor = DecimalExtensions.Random(1000, 10000);
            Cedente = "Empresa XYZ";
            Sacado = "Fulano da Silva";
            Agencia = "1234-5";
            Conta = "6789-0";
            CodigoDeBarras = "123456789012345678901234567890";
            LinhaDigitavel = "1234.5678.9012.3456-7890";
            Instrucoes = "Pagar até o vencimento";
            Multa = DecimalExtensions.Random(5, 10);
            Juros = DecimalExtensions.Random(10, 20);
            Desconto = DecimalExtensions.Random(5, 10);
        }
    }
}
