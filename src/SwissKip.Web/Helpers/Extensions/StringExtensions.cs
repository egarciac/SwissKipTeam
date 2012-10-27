namespace SwissKip.Web.Helpers.Extensions
{
    using System.Linq;

    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            return input.First().ToString().ToUpper() + string.Join("", input.Skip(1));
        }
    }
}