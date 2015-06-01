using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelNWT.Models;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Configuration;
namespace HotelNWT.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public ActionResult Index()
        {
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Reservation", "Home");
            }

            return RedirectToAction("Login3", "Home");

        }
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            foreach (var cookie in Request.Cookies.AllKeys)
            {
                Request.Cookies.Remove(cookie);
            }
            foreach (var cookie in Response.Cookies.AllKeys)
            {
                Response.Cookies.Remove(cookie);
            }
            return RedirectToAction("Login3", "Home");
        }

        [HttpPost]
        public JsonResult UserLogin(LoginData d)
        {
            using (masterEntities dc = new masterEntities())
            {   
                var user = dc.user.Where(a => a.username.Equals(d.Username) && a.password.Equals(d.Password)).FirstOrDefault();

                var model = new LoginData();

                if (user != null)
                {
                    model.Username = user.username;
                    model.Password = user.password;
                    model.RememberMe = d.RememberMe;

                    var authticket = new
                        FormsAuthenticationTicket(1,
                            user.username,
                            DateTime.Now,
                            DateTime.Now.AddYears(1),
                            model.RememberMe,
                            "",
                            FormsAuthentication.FormsCookiePath);

                    string hash = FormsAuthentication.Encrypt(authticket);

                    var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (authticket.IsPersistent) authcookie.Expires = authticket.Expiration;

                    Response.Cookies.Add(authcookie);

                   
                }

                return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                

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
                        u.activated_date = DateTime.Now;
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

     

        [HttpPost]
        public JsonResult Reservation(reservation r)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                using (masterEntities dc = new masterEntities())
                {

                    var reservation = (from u in dc.reservation
                                where u.from_date == r.from_date && u.type == r.type 
                                select u).FirstOrDefault();

                   if (reservation == null)
                    {
                        try
                        {
                            var user = (from u in dc.user
                                        where u.username == System.Web.HttpContext.Current.User.Identity.Name
                                               select u).FirstOrDefault();

                            if (user != null)
                            {
                                r.user_iduser = user.iduser;
                            }

                            dc.reservation.Add(r);
                            dc.SaveChanges();
                            message = "Successful reservation!";
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
                       message = "Room is reservated!";
                   }
 
                }
            }
            else{
                message = "Reservation failed!";
            }
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        [HttpPost]
        public JsonResult FoodOrder(food_order f)
        {
            
            string message = "";
            if (ModelState.IsValid)
            {
                using (masterEntities dc = new masterEntities())
                {
                        try
                        {
                            var user = (from u in dc.user
                                        where u.username == System.Web.HttpContext.Current.User.Identity.Name
                                        select u).FirstOrDefault();
                           
                            var res = (from re in dc.reservation
                                        where re.user.username == System.Web.HttpContext.Current.User.Identity.Name 
                                        select re).FirstOrDefault();
                            var fm = (from m in dc.food_menu
                                      where m.idfood == f.food_menu_idfood
                                      select m).FirstOrDefault();

                            var room = res.type;
                            var name = user.username.ToString() + "from room:" + room.ToString();
                            f.order_name = name;
                            
                            f.order_price = fm.price * f.amount;
                            
                            f.user_iduser = user.iduser;
                            
                            f.oder_date = DateTime.Now;
                            dc.food_order.Add(f);
                            dc.SaveChanges();
                            message = "Successful order!";
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
            }
            else
            {
                message = "Order failed!";
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}
