using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.CustomExceptions
{
    public class ValueDoesNotExistExeception : Exception
    {
        public ValueDoesNotExistExeception()
            : base("Value Already Exists.")
        {
        }
        public ValueDoesNotExistExeception(string message)
            : base(message)
        {
        }

        public ValueDoesNotExistExeception(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
