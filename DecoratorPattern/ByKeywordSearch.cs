using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
namespace DecoratorPattern
{
    public class ByKeywordSearch : AdvancedSearchDecorator
    {
        public ByKeywordSearch()
        {
        }

        public ByKeywordSearch(AdvancedSearchComponent component)
            : base(component)
        {
        }

        public string Keyword
        {
            get;
            set;
        }

        public override List<Product> Search(List<Product> data)
        {
            List<Product> result = data.Where(x => x.ProductName.ToLower().Contains(Keyword.ToLower())).ToList<Product>();

            if (AdvancedSearchComponent != null)
            {
                result = AdvancedSearchComponent.Search(result.ToList<Product>());
            }

            return result;
        }
    }
}
