using System;
using System.Collections.Generic;
using System.Text;

namespace TalkNest.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message)
        {
        }
    }
}
