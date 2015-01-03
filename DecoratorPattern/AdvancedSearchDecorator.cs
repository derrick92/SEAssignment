using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecoratorPattern
{
    public abstract class AdvancedSearchDecorator : AdvancedSearchComponent
    {
        public AdvancedSearchComponent AdvancedSearchComponent
        {
            get;
            set;
        }

        public AdvancedSearchDecorator()
        { }

        public AdvancedSearchDecorator(AdvancedSearchComponent component)
        {
            AdvancedSearchComponent = component;
        }

    }
}
