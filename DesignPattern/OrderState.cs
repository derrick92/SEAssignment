using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public class OrderState
    {
        public IOrderState _CurrentState;
        public OrderState()
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
