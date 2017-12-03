using System.Globalization;

namespace CommonClientLib.ExtensionMethods
{
    public static class IntExtensionMethods
    {
        public static string ToStringWithComma(this int num)
        {
            return string.Format($"{num:#,###}", CultureInfo.InvariantCulture);
        }
    }
}
