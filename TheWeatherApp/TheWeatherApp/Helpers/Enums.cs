using System;
using System.Reflection;

namespace TheWeatherApp.Helpers
{
    public static class Enums
    {
        public enum LocationErrorMessage
        {
            [EnumDesc("Unknown location.\nIf location service is not turned on, please turn it on.")]
            PositionUnavailable  =1,
            [EnumDesc("User has denied to use current location.\nPlease turn on location services for this app.")]
            Unauthorized = 2,
            [EnumDesc("Unknown network error.\nIf location service is not turned on, please turn it on.")]
            UnknownNetworkError = 3
        }
    }

    public static class EnumExtensions
    {
        public static string Description(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            var info =  type.GetRuntimeField(enumeration.ToString());

            var attributes = info?.GetCustomAttributes(typeof(EnumDesc), false);

            if (attributes != null)
            {
                foreach (Attribute item in attributes)
                {
                    if (item is EnumDesc)
                        return (item as EnumDesc).Text;
                }
            }

            return enumeration.ToString();
        }
    }

    public class EnumDesc : Attribute
    {
        public string Text
        {
            get { return _text; }
        }

        private readonly string _text;
        public EnumDesc(string text)
        {
            _text = text;
        }
    }
}

