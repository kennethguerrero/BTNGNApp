using System;
using System.Runtime.Serialization;

namespace BTNGNApp.Exceptions
{
    public class NoInternetException : Exception
    {
        public NoInternetException()
        {
        }

        public NoInternetException(string message) : base(message)
        {
        }

        public NoInternetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoInternetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
