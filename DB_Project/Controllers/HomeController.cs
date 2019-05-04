using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB_Project.Models;
using System.Data.SqlClient;
using System.IO;

namespace DB_Project.Controllers
{
    public class HomeController : Controller
    {
        //ActionResults with Views
        public ActionResult ActorDetails()
        {
            int actorId = 1;
            Actor a = CRUDactor.DisplayActorFunc(actorId);
            return View(a);
        }
        public ActionResult AddMovie()
        {
            if (Session["uType"] == null)//user already logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
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
        public ActionResult Error(int param)
        {
            return View(param);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (Session["uId"] != null)//user already logged in
            {
                return RedirectToAction("Index");
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
                return RedirectToAction("Error", new { param = 1 });
            }
        }
        public ActionResult Signup()
        {
            if (Session["uId"] != null)//user already logged in
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult ViewComplaints()
        {
            if (Session["uType"] == null)//user already logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
                {
                    List<Complaint> clist = CRUDcomplaint.ShowComplaintFunc();
                    return View(clist);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
        
        //ActionResult without Views
        public ActionResult SignupAction(string email, string name, string usertype, string dateOfBirth, string password)
        {
            int ret;
            ret = CRUDuser.SignupFunc(email, name, usertype, dateOfBirth, password);
            if (ret == 1)//user signed up successfully, goto login page
            {
                return RedirectToAction("Login");
            }
            else if (ret == -1)//DB connection failed
            {
                return RedirectToAction("Error", new { param = -1 });
            }
            else
            {
                return RedirectToAction("Error", new { param = 3 });
            }
        }
        public ActionResult LoginAction(string email, string password)
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
            else if (u.ret == -1)//DB connection failed
            {
                return RedirectToAction("Error", new { param = -1 });
            }
            else
            {
                return RedirectToAction("Error", new { param = 4 });
            }
        }

        //Test ActionResult
        public ActionResult test()
        {
            Movie m = CRUDmovie.MovieDetailFunc(1);
            return View(m);
        }
        public ActionResult AddImage()
        {
            return View();
        }
        public ActionResult ImageUpload(HttpPostedFileBase fileToUpload)
        {
            if (fileToUpload != null)
            {
                //change this accordingly with the entered movieID
                string pic = System.IO.Path.GetFileName(fileToUpload.FileName);
                //pic = "random.jpg";

                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/"), pic);
                fileToUpload.SaveAs(path);
            }
            // Redirect to whereever wanted
            return RedirectToAction("Index");
        }

        //JsonResults
        public JsonResult CheckEmailAvailable(string email)
        {
            int ret = CRUDuser.EmailAvailableFunc(email);
            if (ret == 1)//email availbale
            {
                return Json(1);
            }
            else if (ret == -1)//server connection failed
            {
                return Json(-1);
            }
            else//email exists
            {
                return Json(0);
            }
        }
        public JsonResult AddComplaint(string message)
        {
            int ret = CRUDcomplaint.AddComplaintFunc(Session["uId"].ToString(), message);
            if (ret == 1)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
    }
}