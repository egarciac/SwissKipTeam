namespace SwissKip.Web.Core.Exceptions
{
    using System;

    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
            Key = "";
        }

        public ValidationException(string key, string message)
            : base(message)
        {
            Key = key;
        }

        public string Key { get; private set; }

    }
}