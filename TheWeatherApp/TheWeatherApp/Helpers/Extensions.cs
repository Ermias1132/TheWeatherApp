using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheWeatherApp.Helpers
{
    public static class Extensions
    {
        public static bool IsEmpty(this string val)
        {
            return string.IsNullOrWhiteSpace(val);
        }

        public static double ToDouble(this string val)
        {
            return Convert.ToDouble(val);
        }
        
        public static bool IsValidZipCode(this string zipCode)
        {
            var _usZipRegEx = @"\d{5}";

            bool validZipCode = Regex.Match(zipCode, _usZipRegEx).Success;
            return validZipCode;
        }
    }
}
