using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelNWT.Models;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Diagnostics;
namespace HotelNWT.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UserLogin(LoginData d)
        {
            using (masterEntities dc = new masterEntities())
            {
                
                var user = dc.user.Where(a => a.username.Equals(d.Username) && a.password.Equals(d.Password)).FirstOrDefault();
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                //var authticket = new
                //    FormsAuthenticationTicket(1,
                //        "admin",
                //        DateTime.Now,
                //        DateTime.Now.AddYears(1),
                //        true,
                //        "",
                //        FormsAuthentication.FormsCookiePath);

                //string hash = FormsAuthentication.Encrypt(authticket);

                //var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                //if (authticket.IsPersistent) authcookie.Expires = authticket.Expiration;

                //Response.Cookies.Add(authcookie);

                //if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                //{
                //    return RedirectToAction("Search", "Search");
                //}

            }
        }

        [HttpPost]
        public JsonResult Register(user u)
        {
            string message = "";
            //Here we will save data to the database
            if (ModelState.IsValid)
            {
                using (masterEntities dc = new masterEntities())
                {
                    //check username available
                    var user = dc.user.Where(a => a.username.Equals(u.username)).FirstOrDefault();
                    if (user == null)
                    {
                        //Save here
                        u.confirmation_key = "1";
                        u.created_date = DateTime.Now;
                    try
                    {
                        dc.user.Add(u);
                        dc.SaveChanges();
                        message = "Success";
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                    }
                    }
                    else
                    {
                        message = "Username not available!";
                    }
                    }
                    }
                    else
                    {
                        message = "Failed!";
                    }
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
        


    }
}
