using System.Web.Mvc;
using System.Web.Security;
using Common;
using BusinessLayer;


namespace SEImplementation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult Register()
        {
            return View(new Models.RegisterModel());
        }

        [HttpPost]
        public ActionResult Register(Models.RegisterModel rm)
        {
            if (new UserBL().DoesEmailExist(rm.UserName) && new UserBL().DoesUserNameExist(rm.UserName)) { return Redirect("/home/register?registered=userandemailtaken"); }
            else if (new UserBL().DoesEmailExist(rm.UserName)) { return Redirect("/home/register?registered=emailtaken"); }
            else if (new UserBL().DoesUserNameExist(rm.UserName)) { return Redirect("/home/register?registered=usernametaken"); }
            else
            {
                User u = new User();
                u.Username = rm.UserName;
                u.FirstName = rm.FirstName;
                u.Surname = rm.Surname;
                u.Email = rm.Email;
                u.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(rm.Password, "MD5");
                u.MobileNum = rm.MobileNum;

                new UserBL().Create(u);
                return Redirect("/?registered=success");
            }
        }

    }
}
