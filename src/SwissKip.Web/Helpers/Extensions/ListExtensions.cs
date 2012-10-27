namespace SwissKip.Web.Helpers.Extensions
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class ListExtensions
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> collection)
        {
            return new SelectList(collection, "Id", "Name");
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> collection, string selectedValue)
        {
            return new SelectList(collection, "Id", "Name", selectedValue);
        }
    }
}