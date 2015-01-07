using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.CustomExceptions
{
    public class ValueAlreadyExistsException:Exception
    {
                public ValueAlreadyExistsException()
            : base("Value Already Exists.")
        {
        }
        public ValueAlreadyExistsException(string message)
            : base(message)
        {
        }

        public ValueAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
