using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public class Approved : IOrder
    {
        private readonly Order _Parent;
        public Approved(Order OrderState)
        {
            _Parent = OrderState;
            this.Approve();
        }
        public string NewOrderPlaced()
        {
            throw new Exception("OrderState has already been placed");
        }
        public string Dispatch()
        {
            _Parent._CurrentState = new Dispatched(_Parent);
            return _Parent._CurrentState.Dispatch();
        }
        public string Register()
        {
            throw new Exception("OrderState has already been registered");
        }
        public string Approve()
        {
            return "Approved";
        }
    }
}
