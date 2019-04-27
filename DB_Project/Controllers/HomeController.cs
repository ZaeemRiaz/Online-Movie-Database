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
        public ActionResult Msg(int param)
        {
            return View(param);
        }
        public ActionResult SignupAction(string email, string name, string usertype, string dateOfBirth, string password)
        {
            int ret = 4;
            ret = CRUDuser.SignupFunc(email, name, usertype, dateOfBirth, password);
            return RedirectToAction("Msg", new { param = ret });
        }
        public ActionResult LoginAction(string email, string password)
        {
            int ret = 3;
            ret = CRUDuser.LoginFunc(email, password);
            ret += 2;
            return RedirectToAction("Msg", new { param = ret });
        }
    }
}