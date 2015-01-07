using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.CustomExceptions
{
    public class NullValueException:Exception
    {
                public NullValueException()
            : base("The value are empty.")
        {
        }
        public NullValueException(string message)
            : base(message)
        {
        }

        public NullValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
