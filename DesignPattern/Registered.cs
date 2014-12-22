using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public class Registered : IOrderState
    {
        private readonly OrderState _Parent;
        public string NewOrderPlaced()
        {
            throw new Exception("OrderState has already been placed");
        }
        public Registered(OrderState OrderState)
        {
            _Parent = OrderState;
            this.Register();
        }
        public string Dispatch()
        {
            throw new Exception("OrderState has not been registered yet");
        }
        public string Register()
        {
            return "Registered";
        }
        public string Approve()
        {
            _Parent._CurrentState = new Approved(_Parent);
            return _Parent._CurrentState.Approve();
        }
    }
}
