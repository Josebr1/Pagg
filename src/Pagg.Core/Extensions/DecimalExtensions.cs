namespace Pagg.Core.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal Random(decimal minValue, decimal maxValue)
        {
            Random random = new();
            double valorDouble = random.NextDouble() * (double)(maxValue - minValue) + (double)minValue;
            return Convert.ToDecimal(valorDouble);
        }
    }
}
