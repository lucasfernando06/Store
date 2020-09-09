using System;

namespace Store.Utils
{
    public static class FormatUtils
    {
        public static string FormatValue(this decimal value)
        {
            return "R$ " + value.FormatDecimal(2);
        }

        public static string FormatDecimal(this decimal value, int casasDecimais)
        {
            return value.ToString("N2");                 
        }
    }
}
