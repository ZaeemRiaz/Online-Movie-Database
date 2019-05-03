using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB_Project.Models;
using System.Data.SqlClient;

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
            if (Session["uId"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult MovieDetails(int movieID)
        {
            if (movieID != 0)
            {
                movieDetailStruct mdstruct = new movieDetailStruct();
                mdstruct.movieDetail = CRUDmovie.MovieDetailFunc(movieID);
                mdstruct.cast = CRUDactor.MovieCastFunc(movieID);
                mdstruct.commentList = CRUDcomment.MovieCommentFunc(movieID);
                return View(mdstruct);
            }
            else
            {
                return RedirectToAction("Msg", new { param = 1 });
            }
        }
        public ActionResult ActorDetails()
        {
            int actorId = 1;
            Actor a = CRUDactor.DisplayActorFunc(actorId);
            return View(a);
        }
        public ActionResult Error(int param)
        {
            return View(param);
        }
        public ActionResult SignupAction(string email, string name, string usertype, string dateOfBirth, string password)
        {
            if (Session["uId"] != null)//user already logged in
            {
                return RedirectToAction(Session["LoginRedirect"].ToString());
            }
            else
            {
                int ret;
                ret = CRUDuser.SignupFunc(email, name, usertype, dateOfBirth, password);
                if (ret == 1)//user signed up successfully, goto login page
                {
                    return RedirectToAction("Login");
                }
                else//print error msg
                {
                    return RedirectToAction("Msg", new { param = ret });
                }
            }
        }
        public ActionResult LoginAction(string email, string password)
        {
            if (Session["uId"] != null)//user already logged in
            {
                return RedirectToAction(Session["LoginRedirect"].ToString());
            }
            else
            {
                userLoginStruct u = null;
                u = CRUDuser.LoginFunc(email, password);
                //store session info after login
                if (u.ret != 0 && u.ret != -1)
                {
                    Session["uId"] = u.id;
                    Session["uType"] = u.type;
                    if (Session["LoginRedirect"] != null)//goto prev page login or signup called from
                    {
                        return RedirectToAction(Session["LoginRedirect"].ToString());
                    }
                    else//goto homepage if not redirect availabe
                    {
                        return RedirectToAction("Index");
                    }
                }
                else//print error msg
                {
                    return RedirectToAction("Msg", new { param = u.ret });
                }
            }
        }

        public ActionResult test()
        {
            Movie m = CRUDmovie.MovieDetailFunc(1);
            return View(m);
        }
    }
}