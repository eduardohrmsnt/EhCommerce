namespace EhCommerce.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string FormatWith(this string str, params object[] args) 
        { 
            return string.Format(str, args);
        }
    }
}
