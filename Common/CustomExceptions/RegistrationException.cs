using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CustomExceptions
{
    public class RegistrationException:Exception
    {
        public RegistrationException()
            : base("An error occured during Registration of a new user.")
        {
        }
        public RegistrationException(string message)
            : base(message)
        {
        }

        public RegistrationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
