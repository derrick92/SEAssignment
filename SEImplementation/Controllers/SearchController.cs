using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLayer;
using DecoratorPattern;

namespace SEImplementation.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index(string val)
        {
            List<Product> data = new ProductBL().AllProducts().ToList();
            AdvancedSearchDecorator search = 
                new ByKeywordSearch( new ByTypeSearch() { TypeSearch = typeof(Product) }) { Keyword = val };
            List<Product> result = search.Search(data);
            return View(result);
        }

        public ActionResult SearchProduct(string txtSearch)
        {
            if (txtSearch == string.Empty)
            {
                return Redirect("/search/?val=NoSearchValue");
            }
            return Redirect("/search/?val=" + txtSearch);
        }

    }
}
