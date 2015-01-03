using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DecoratorPattern
{
    public class ByTypeSearch : AdvancedSearchDecorator
    {
        public ByTypeSearch()
        {
        }

        public ByTypeSearch(AdvancedSearchComponent component)
            : base(component)
        {
        }

        public Type TypeSearch
        {
            get;
            set;
        }

        public override List<Product> Search(List<Product> data)
        {
            List<Product> result =
                                data.Where(
                                        x => x.GetType() == TypeSearch).ToList<Product>();

            if (AdvancedSearchComponent != null)
            {
                result = AdvancedSearchComponent.Search(result.ToList<Product>());
            }

            return result;
        }
    }
}
