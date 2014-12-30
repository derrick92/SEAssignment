using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public interface IOrder
    {
        string NewOrderPlaced();
        string Register();
        string Dispatch();
        string Approve();
    }
}
