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
        public ActionResult ActorDetails(string actorID)
        {
            Actor a = CRUDactor.DisplayActorFunc(actorID);
            if (a != null)//actor found
            {
                return View(a);
            }
            else//actor not found
            {
                return RedirectToAction("Error", new { param = 2 });
            }
        }
        public ActionResult AddComplaint()
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
        public ActionResult AddMovie()
        {
            if (Session["uType"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
                {
                    return View();
                }
                else//user not admin
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult EditMovieDetails(string movieID)
        {
            if (Session["uType"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
                {
                    Session["movieID"] = movieID;
                    return View(movieID);
                }
                else//user not admin
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Error(Nullable<int> param)
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
        public ActionResult MovieDetails(string movieID)
        {
            if (movieID != "0")
            {
                movieDetailStruct mdstruct = new movieDetailStruct();
                mdstruct.movieDetail = CRUDmovie.MovieDetailFunc(movieID);
                mdstruct.cast = CRUDactor.MovieCastFunc(movieID);
                mdstruct.commentList = CRUDcomment.MovieCommentFunc(movieID);
                if (mdstruct.movieDetail != null)//movie found
                {
                    return View(mdstruct);
                }
                else//movie not found
                {
                    return RedirectToAction("Error", new { param = 1 });
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
            if (Session["uType"] == null)//user not logged in
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
                else//user not admin
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
            if (Session["uId"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
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
        }
        public ActionResult AddMovieAction(string title, string genre, string releaseDate, string description, HttpPostedFileBase fileToUpload)
        {
            if (Session["uType"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
                {
                    string pic = null;
                    if (fileToUpload != null)
                    {
                        //change this accordingly with the entered movieID
                        //string pic = System.IO.Path.GetFileName(fileToUpload.FileName);
                        pic = title + ".jpg";
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/"), pic);
                        fileToUpload.SaveAs(path);
                    }
                    int ret = CRUDmovie.AddMovieFunc(title, description, genre, releaseDate, pic);
                    if (ret == 1)//successfully added movie
                    {
                        return RedirectToAction("Index");
                    }
                    else if (ret == -1)//DB connection failed
                    {
                        return RedirectToAction("Error", new { param = -1 });
                    }
                    else//add movie failed
                    {
                        return RedirectToAction("Error", new { param = 4 });
                    }
                }
                else//user not admin
                {
                    return RedirectToAction("Index");
                }
            } 
        }
        public ActionResult EditMovieTitleAction(string title)
        {
            if (Session["uType"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
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
                        return RedirectToAction("Error", new { param = 9 });
                    }
                }
                else//user not admin
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult EditMovieGenreAction(string genre)
        {
            if (Session["uType"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
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
                        return RedirectToAction("Error", new { param = 9 });
                    }
                }
                else//user not admin
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult EditMovieDescriptionAction(string description)
        {
            if (Session["uType"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
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
                        return RedirectToAction("Error", new { param = 9 });
                    }
                }
                else//user not admin
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult EditMovieDateofReleaseAction(string releaseDate)
        {
            if (Session["uType"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
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
                        return RedirectToAction("Error", new { param = 9 });
                    }
                }
                else//user not admin
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult DeletMovieAction(string movieID)
        {
            if (Session["uType"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["uType"].ToString() == "A")//admin
                {
                    int ret = CRUDmovie.DelMovieFunc(movieID);
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
                        return RedirectToAction("Error", new { param = 10 });
                    }
                }
                else//user not admin
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult AddRatingAction(string movieID, string rating)
        {
            if (Session["uId"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                int ret = CRUDrating.AddRatingFunc(movieID, Session["uId"].ToString(), rating);
                if (ret == 1)//user signed up successfully, goto login page
                {
                    return RedirectToAction("MovieDetails", new { movieID = movieID});
                }
                else if (ret == -1)//DB connection failed
                {
                    return RedirectToAction("Error", new { param = -1 });
                }
                else
                {
                    return RedirectToAction("Error", new { param = 7 });
                }
            }
        }
        public ActionResult AddCommentAction(string movieID, string comment)
        {
            if (Session["uId"] == null)//user not logged in
            {
                return RedirectToAction("Login");
            }
            else
            {
                int ret = CRUDcomment.AddCommentFunc(movieID, Session["uId"].ToString(), comment);
                if (ret == 1)//user signed up successfully, goto login page
                {
                    return RedirectToAction("MovieDetails", new { movieID = movieID });
                }
                else if (ret == -1)//DB connection failed
                {
                    return RedirectToAction("Error", new { param = -1 });
                }
                else
                {
                    return RedirectToAction("Error", new { param = 8 });
                }
            }
        }

        //JsonResults
        public JsonResult CheckEmailAvailable(string email)
        {
            int ret = CRUDuser.EmailAvailableFunc(email);
            return Json(ret);
        }
    }
}