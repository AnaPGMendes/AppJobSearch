using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobSearch.App.Utility.Converters
{
    class TextToDoubleConverter
    {
        public static Double ToDouble(string value)
        {
            if (value != null) { 
                // Remover todas as letras.
                value = RemoveExtraText(value);
                // Conversão de String para Double
                return Double.Parse(value);
            }
            return 0;
        }

        private static string RemoveExtraText(string value)
        {
            var allowedChars = "01234567890.,";
            return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
        }
    }
}

