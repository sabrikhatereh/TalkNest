using System;
using System.Globalization;

namespace TalkNest.Application.Exceptions
{
    public class InternalServerException : CustomException
    {
        public InternalServerException() : base() { }

        public InternalServerException(string message) : base(message) { }

        public InternalServerException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}