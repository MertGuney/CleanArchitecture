using System.Globalization;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException() : base()
        {
        }

        public ApplicationException(string message) : base(message)
        {
        }

        public ApplicationException(string message, Exception exception) : base(message, exception)
        {
        }

        public ApplicationException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
