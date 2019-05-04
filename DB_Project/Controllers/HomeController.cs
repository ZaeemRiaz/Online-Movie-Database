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
        public ActionResult AddComplaint()
        {
            /*
            if (Session["uId"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
            */
            return View();
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
        public ActionResult EditMovieDetails(int param)
        {
            Session["movieID"] = param; 
            return View(param);
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
                if (mdstruct.movieDetail == null)
                {
                    return RedirectToAction("Error", new { param = 1 });
                }
                else
                {
                    return View(mdstruct);
                }
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
                if (Session["Redirect"] != null)//goto prev page login or signup called from
                {
                    return RedirectToAction(Session["Redirect"].ToString());
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
        public ActionResult AddComplaintAction(string message)
        {
            int ret = CRUDcomplaint.AddComplaintFunc(Session["uId"].ToString(), message);
            if (ret == 1)
            {
                return RedirectToAction("Index");
            }
            else if (ret == -1)//DB connection failed
            {
                return RedirectToAction("Error", new { param = -1 });
            }
            else
            {
                return RedirectToAction("Error", new { param = 5 });
            }
        }
        public ActionResult AddMovieAction(string title, string genre, string releaseDate, string description, HttpPostedFileBase fileToUpload)
        {
            string pic = null;
            if (fileToUpload != null)
            {
                //change this accordingly with the entered movieID
                //string pic = System.IO.Path.GetFileName(fileToUpload.FileName);
                pic = title+".jpg";
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/"), pic);
                fileToUpload.SaveAs(path);
            }
            int ret = CRUDmovie.AddMovieFunc(title, description, genre, releaseDate, pic);
            if (ret == 1)
            {
                return RedirectToAction("Index");
            }
            else if (ret == -1)//DB connection failed
            {
                return RedirectToAction("Error", new { param = -1 });
            }
            else
            {
                return RedirectToAction("Error", new { param = 4 });
            }
        }
        public ActionResult EditMovieTitleAction(string title)
        {

            int ret = CRUDmovie.EditMovieTitleFunc(Session["movieID"].ToString(), title);
            if (ret == 1)
            {
                return RedirectToAction("Index");
            }
            else if (ret == -1)//DB connection failed
            {
                return RedirectToAction("Error", new { param = -1 });
            }
            else
            {
                return RedirectToAction("Error", new { param = 6 });
            }
        }
        public ActionResult EditMovieGenreAction(string genre)
        {

            int ret = CRUDmovie.EditMovieGenreFunc(Session["movieID"].ToString(), genre);
            if (ret == 1)
            {
                return RedirectToAction("Index");
            }
            else if (ret == -1)//DB connection failed
            {
                return RedirectToAction("Error", new { param = -1 });
            }
            else
            {
                return RedirectToAction("Error", new { param = 6 });
            }
        }
        public ActionResult EditMovieDescriptionAction(string description)
        {

            int ret = CRUDmovie.EditMovieDescriptionFunc(Session["movieID"].ToString(), description);
            if (ret == 1)
            {
                return RedirectToAction("Index");
            }
            else if (ret == -1)//DB connection failed
            {
                return RedirectToAction("Error", new { param = -1 });
            }
            else
            {
                return RedirectToAction("Error", new { param = 6 });
            }
        }
        public ActionResult EditMovieDateofReleaseAction(string releaseDate)
        {

            int ret = CRUDmovie.EditMovieDateofReleaseFunc(Session["movieID"].ToString(), releaseDate);
            if (ret == 1)
            {
                return RedirectToAction("Index");
            }
            else if (ret == -1)//DB connection failed
            {
                return RedirectToAction("Error", new { param = -1 });
            }
            else
            {
                return RedirectToAction("Error", new { param = 6 });
            }
        }

        //JsonResults
        public JsonResult CheckEmailAvailable(string email)
        {
            int ret = CRUDuser.EmailAvailableFunc(email);
            return Json(ret);
        }

        //Test ActionResult
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

    }
}