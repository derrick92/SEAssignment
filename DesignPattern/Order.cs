using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public class Order
    {
        public IOrder _CurrentState;
        public Order()
        {
            _CurrentState = new NewOrder(this);
        }
        public string Dispatch()
        {
            return _CurrentState.Dispatch();
        }
        public string Register()
        {

            return _CurrentState.Register();
        }
        public string Approve()
        {
            return _CurrentState.Approve();
        }
    } 

}
