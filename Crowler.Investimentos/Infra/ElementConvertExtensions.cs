using System;
using System.Globalization;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace Crowler.Investimentos.Infra
{
    public static class ElementConvertExtensions
    {
        private static CultureInfo culture = new CultureInfo("pt-BR");

        public static double TextContentToDouble(this IElement element)
        {
            var value = element.TextContent.Trim();
            string pattern = "[^0-9,]";
            value = Regex.Replace(value, pattern, String.Empty);
            return Double.Parse(value, culture);
        }

        public static int TextContentToInteger(this IElement element)
        {
            var value = element.TextContent.Trim();
            string pattern = "[^0-9]";
            value = Regex.Replace(value, pattern, String.Empty);
            return Convert.ToInt32(value);
        }

        public static string TextContentToString(this IElement element)
        {
            return element.TextContent.Trim();
        }
    }
}