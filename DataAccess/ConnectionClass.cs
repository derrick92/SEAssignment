using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class ConnectionClass
    {
        public SEshopEntities Entity;

        public ConnectionClass()
        {
            Entity = new SEshopEntities();
        }
    }
}
