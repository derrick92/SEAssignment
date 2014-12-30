using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEImplementation.Models;
using System.IO;
using Common;
using BusinessLayer;
using SEImplementation.Classes;

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
                string dbPath = "~/Layout/productimgs/noImg.jpg";
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

        public ActionResult myproducts()
        {
            User u = new UserBL().GetUserByUsername(User.Identity.Name);
            List<Product> products = new ProductBL().GetProductsByCreator(u.UserID).ToList();
            return View(products);
        }

        public ActionResult Delete(int prodid)
        {
            try
            {
                new ProductBL().DeleteProduct(prodid);
                return Redirect("/Product/myproducts?msg=productdeletedsuccessfully");
            }
            catch
            {
                return Redirect("/Product/myproducts?msg=productdeletedunsuccessfully");
            }
        }




        public ProductModel editProductLoad(int prodid)
        {
            Product p = new ProductBL().GetProductById(prodid);
            User u = new UserBL().GetUserByID(p.CreatedBy);
            ProductModel pm = new ProductModel();

            pm.productID = p.ProductID;
            pm.ProductName = p.ProductName;
            pm.ProductStock = p.ProductStock;
            pm.CreatedBy = p.CreatedBy;
            pm.DateAdded = p.DateAdded;
            pm.userName = u.Username;
            pm.ProductImage = p.ProductImage;
            pm.ProductDesc = p.ProductDesc;
            pm.ProductPrice = p.ProductPrice;

            return pm;
        }

        public ActionResult Edit(int prodid)
        {
            ProductModel pm = editProductLoad(prodid);
            if (new RoleChecker().checkIfAdmin(User.Identity.Name))
            {

                try
                {
                    return View(pm);
                }
                catch
                {
                    return Redirect("/product?msg=producteditnotsaved");
                }
            }
            else
            {
                User ux = new UserBL().GetUserByUsername(User.Identity.Name);
                if (pm.CreatedBy != ux.UserID)
                {
                    return Redirect("/product?msg=notyourproduct");
                }
                else
                {
                    try
                    {
                        return View(pm);
                    }
                    catch
                    {
                        return Redirect("/product?msg=producteditnotsaved");
                    }
                }
            }
        }


        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, ProductModel pm)
        {
            if (new RoleChecker().checkIfAdmin(User.Identity.Name))
            {
                try
                {
                    EditMethod(file, pm);
                    return Redirect("/product?msg=producteditedsuccessfully");
                }
                catch
                {
                    return Redirect("/product?msg=producteditnotsaved");
                }
            }
            else
            {
                User u = new UserBL().GetUserByUsername(User.Identity.Name);
                if (pm.CreatedBy != u.UserID)
                {
                    return Redirect("/product?msg=notyourproduct");
                }
                else
                {
                    try
                    {
                        EditMethod(file, pm);
                        return Redirect("/product?msg=producteditedsuccessfully");
                    }
                    catch
                    {
                        return Redirect("/product?msg=producteditnotsaved");
                    }
                }
            }


        }



        public void EditMethod(HttpPostedFileBase file, ProductModel pm)
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
            else
            {
                dbPath = pm.ProductImage;
            }

            Product p = new ProductBL().GetProductById(pm.productID); ;
            User u = new UserBL().GetUserByUsername(User.Identity.Name);



            p.ProductName = pm.ProductName;
            p.ProductDesc = pm.ProductDesc;
            p.ProductStock = pm.ProductStock;
            p.ProductImage = dbPath;
            p.ProductPrice = pm.ProductPrice;

            new ProductBL().Update(p);
        }





        public ActionResult Details(int prodid)
        {
            Product p = new ProductBL().GetProductById(prodid);
            User u = new UserBL().GetUserByID(p.CreatedBy);
            ProductModel pm = new ProductModel();

            pm.productID = p.ProductID;
            pm.ProductName = p.ProductName;
            pm.ProductStock = p.ProductStock;
            pm.CreatedBy = p.CreatedBy;
            pm.DateAdded = p.DateAdded;
            pm.userName = u.Username;
            pm.ProductImage = p.ProductImage;
            pm.ProductDesc = p.ProductDesc;
            pm.ProductPrice = p.ProductPrice;

            return View(pm);
        }


        public ActionResult AddtoCart(string username, int prodID, int qty)
        {
            User x = new UserBL().GetUserByUsername(username);
            ShoppingCart y = new ShoppingCart();
            y.UserID = x.UserID;
            y.ProductID = prodID;
            y.Qty = qty;

            new CartBL().AddToCart(y);
            return RedirectToAction("Index");
        }



    }
}
