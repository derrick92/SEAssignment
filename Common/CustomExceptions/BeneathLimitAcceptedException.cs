using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.CustomExceptions
{
    public class BeneathLimitAcceptedException : Exception
    {
        public BeneathLimitAcceptedException()
            : base("The lenght does is beneath the limit.")
        {
        }
        public BeneathLimitAcceptedException(string message)
            : base(message)
        {
        }

        public BeneathLimitAcceptedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
