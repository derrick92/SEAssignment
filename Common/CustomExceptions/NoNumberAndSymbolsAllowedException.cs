using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.CustomExceptions
{
    public class NoNumberAndSymbolsAllowedException : Exception
    {
        public NoNumberAndSymbolsAllowedException()
            : base("No numbers & symbols allowed.")
        {
        }
        public NoNumberAndSymbolsAllowedException(string message)
            : base(message)
        {
        }

        public NoNumberAndSymbolsAllowedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
