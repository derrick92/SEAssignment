using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public class NewOrder : IOrderState
    {
        private readonly OrderState _Parent;
        public NewOrder(OrderState OrderState)
        {
            _Parent = OrderState;
            this.NewOrderPlaced();

        }
        public bool IsDispatched
        {
            get { return false; }
        }
        public string NewOrderPlaced()
        {
            return "New Order Placed";
        }
        public string Dispatch()
        {
            _Parent._CurrentState = new Dispatched(_Parent);
            return _Parent._CurrentState.Dispatch();
        }
        public string Register()
        {
            _Parent._CurrentState = new Registered(_Parent);
            return _Parent._CurrentState.Register();
        }
        public string Approve()
        {
            _Parent._CurrentState = new Approved(_Parent);
            return _Parent._CurrentState.Approve();
        }
    }
}
