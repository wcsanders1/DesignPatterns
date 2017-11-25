using System.Globalization;

namespace CommonClientLib
{
    public static class IntExtensionMethods
    {
        public static string ToStringWithComma(this int num)
        {
            return string.Format($"{num:#,###}", CultureInfo.InvariantCulture);
        }
    }
}
