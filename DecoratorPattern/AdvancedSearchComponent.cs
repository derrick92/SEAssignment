using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DecoratorPattern
{
    public abstract class AdvancedSearchComponent
    {
        public abstract List<Product> Search(List<Product> data);
    }
}
