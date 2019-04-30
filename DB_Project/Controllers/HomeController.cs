using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB_Project.Models;

namespace DB_Project.Controllers
{
    public class HomeController : Controller
    {
        //GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Complaint()
        {
            return View();
        }
        public ActionResult MovieDetails()
        {
            return View();
        }
        public ActionResult ActorDetails()
        {
            return View();
        }
        public ActionResult MovieCast()
        {
            return View();
        }
        public ActionResult Msg(int param)
        {
            return View(param);
        }
        public ActionResult SignupAction(string email, string name, string usertype, string dateOfBirth, string password)
        {
            int ret;
            ret = CRUDuser.SignupFunc(email, name, usertype, dateOfBirth, password);
            if (ret == 1)//user signed up successfully
            {
                return RedirectToAction("Login");
            }
            else//error
            {
                return RedirectToAction("Msg", new { param = ret });
            }
        }
        public ActionResult LoginAction(string email, string password)
        {
            int ret;
            ret = CRUDuser.LoginFunc(email, password);
            if (ret == 1)
            {
                Session["UserId"] = 12;//user id
                //check prev or curent session
                //return RedirectToAction(Session["CurrentView"].ToString());
            }
            else
            {
                ret = 2;
                return RedirectToAction("Msg", new { param = ret });
            }
        }
    }
}