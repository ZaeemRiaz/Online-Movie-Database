﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB_Project.Models;

namespace DB_Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult Msg(int param)
        {
            return View(param);
        }
        public ActionResult SignUpfunc(string email, string name, string usertype, string dateOfBirth, string password)
        {
            userProc uproc = new userProc();
            ObjectParameter flag = new ObjectParameter("flag", typeof(int));
            var value = uproc.add_user(name, usertype, email, password, dateOfBirth, flag);
            int ret = Convert.ToInt32(flag.Value);

            return RedirectToAction("Msg", new { param = ret });
        }
        public ActionResult LogInfunc(string email, string password)
        {
            userProc uproc = new userProc();
            ObjectParameter flag = new ObjectParameter("flag", typeof(int));
            var value = uproc.login_user(email, password, flag);
            int ret = Convert.ToInt32(flag.Value);
            ret += 2;

            return RedirectToAction("Msg", new { param = ret });
        }
    }
}