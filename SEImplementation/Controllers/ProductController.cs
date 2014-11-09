using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEImplementation.Models;
using System.IO;
using Common;
using BusinessLayer;

namespace SEImplementation.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            List<Product> products = new ProductBL().AllProducts().ToList();
            return View(products);
        }


        public ActionResult addProduct()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public ActionResult addProduct(HttpPostedFileBase file, ProductModel pm)
        {
            try
            {
                string dbPath = "";
                var r = new Random();
                string random = r.Next(1000000, 9000000).ToString();

                if (file != null && file.ContentLength > 0)
                {
                    // extract only the fielname
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    dbPath = "~/Layout/productimgs/" + random + fileName;
                    var path = Path.Combine(Server.MapPath("~/Layout/productimgs/"), random + fileName);
                    file.SaveAs(path);
                }

                Product p = new Product();
                User u = new UserBL().GetUserByUsername(User.Identity.Name);

                p.ProductName = pm.ProductName;
                p.ProductDesc = pm.ProductDesc;
                p.ProductStock = pm.ProductStock;
                p.ProductImage = dbPath;
                p.ProductPrice = pm.ProductPrice;
                p.DateAdded = DateTime.Now;
                p.CreatedBy = u.UserID;

                new ProductBL().CreateProduct(p);

                return Redirect("/product?msg=productadded");
            }
            catch
            {
                return Redirect("/product?msg=productnotsaved");
            }
        }


        public ActionResult recentProduct()
        {
            List<Product> products = new ProductBL().RecentProductList().ToList();
            return View(products);
        }



    }
}
