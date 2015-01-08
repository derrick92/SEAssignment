using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.CustomExceptions
{
    public class ExceedCharacterLimitExecption : Exception
    {
        public ExceedCharacterLimitExecption()
            : base("Exceeds word Limit.")
        {
        }
        public ExceedCharacterLimitExecption(string message)
            : base(message)
        {
        }

        public ExceedCharacterLimitExecption(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
