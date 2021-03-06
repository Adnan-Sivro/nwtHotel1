﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelNWT.Models;
using System.Net.Mail;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Net;
namespace HotelNWT.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public ActionResult Index()
        {
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated )
            {
                if (System.Web.HttpContext.Current.User.Identity.Name == "admin")
                {
                    return RedirectToAction("ReservationAdmin", "Home");
                }
                return RedirectToAction("Reservation", "Home");
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

                if (user.confirmation_key == "Aktivan")
                {
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
                }
                else 
                {
                    return new JsonResult { Data="notactivated",  JsonRequestBehavior = JsonRequestBehavior.DenyGet };
                }

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
                            DateTime danas = DateTime.Now;
                            if (r.from_date <= danas && r.to_date >= danas)
                            {
                                r.status = 1;
                            }
                            else
                            {
                                r.status = 0;
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
                            var name = user.username.ToString() + " from room: " + room.ToString();
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

        [HttpGet]
        public JsonResult ReservatedRooms()
        {
            List<ReservationData> CO = new List<ReservationData>();
            DateTime danas = DateTime.Now;
            using (masterEntities dc = new masterEntities())
            {
                
                    var reservations = dc.reservation.Where(a => a.status.Equals(1)).OrderBy(a => a.type).ToList();
                        
                foreach (var i in reservations)
                    {


                        CO.Add(new ReservationData
                        {
                            from_date=i.from_date,
                            to_date = i.to_date,
                            type=i.type
                        }
                            );
                    }
               
                
            }
            JsonResult res = new JsonResult { Data = CO, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return res;
        }

        [HttpGet]
        public JsonResult OrderedFood()
        {
            List<FoodOrderData> CO = new List<FoodOrderData>();
            DateTime danas = DateTime.Now;
            using (masterEntities dc = new masterEntities())
            {

                var foodorders = dc.food_order.ToList();

                foreach (var i in foodorders)
                {


                    CO.Add(new FoodOrderData
                    {
                        order_name=i.order_name,
                        oder_date=i.oder_date,
                        amount=i.amount,
                        order_price=i.order_price
                    }
                        );
                }


            }
            JsonResult res = new JsonResult { Data = CO, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return res;
        }


        public ActionResult signOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);



            return RedirectToAction("login3", "home");

        }
        [HttpGet]
        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase ImagePath)
        {
            if (ImagePath != null)
            {
                string pic = System.IO.Path.GetFileName(ImagePath.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/images"), pic);
                ImagePath.SaveAs(path);
                using (masterEntities myContext = new masterEntities())
                {
                    int k = 1;
                    image galleryobj = new image
                    {
                        imagePath = "~/Content/images/" + pic,
                        resource_type_idresource_type = k
                    };

                    myContext.image.Add(galleryobj);
                    myContext.SaveChanges();

                }

            }

            return RedirectToAction("Images", "Home");
        }

        bool POSALjIMAIL = false;

        [HttpPost]
        public JsonResult RegistracijaJson(user korisnik)
        {
            Guid tmpGuid = Guid.NewGuid();
            var parametri = new Dictionary<string, object>{
                {"Username", korisnik.username},
                {"Password", korisnik.password},
                {"Email", korisnik.email},
                {"GUID", tmpGuid.ToString().ToLower()}
            };
            try
            {
                masterEntities dc = new masterEntities();
                korisnik.confirmation_key = "1";
                korisnik.created_date = DateTime.Now;
                korisnik.activated_date = DateTime.Now;
                dc.user.Add(korisnik);
                dc.SaveChanges();
                user u = dc.user.Where(a => a.username == korisnik.username).FirstOrDefault();
                int ID = Convert.ToInt32(u.iduser);
                korisnik.iduser = ID;

                SendEmail("Potvrda Registracije", string.Format(@"
                Dobro došli na našu stranicu i čestitamo na uspješnoj registraciji.
                Da biste potvrdili registraciju, kliknite na link ispod:
                   http://localhost:52474/Data/PotvrdaRegistracijeJson/{0}?guid={1}", korisnik.iduser, tmpGuid), korisnik.email);

                return new JsonResult { Data = "Email za potvrdu registracije je poslan. Provjerite Vaš inbox!", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                Logovi log = new Logovi
                {
                    Datum = DateTime.Now,
                    Sadrzaj = ex.ToString(),
                    Tip = 3
                };
                masterEntities dc = new masterEntities();
                dc.Logovi.Add(log);

                return new JsonResult { Data = "Došlo je do greške", JsonRequestBehavior=JsonRequestBehavior.DenyGet };
            }
        }
        private bool SendEmail(string subject, string body, string mailTo)
        {

            string fromMail = "enesmujic07@gmail.com";

           MailMessage mail = new MailMessage(fromMail, mailTo, subject, body);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Timeout = 20000,
                Credentials = new NetworkCredential("enesmujic07", "07071991enes")
            };
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {

                Logovi log = new Logovi
                {
                    Datum = DateTime.Now,
                    Sadrzaj = ex.ToString(),
                    Tip = 3
                };
                masterEntities dc = new masterEntities();
                dc.Logovi.Add(log);
                return false;
            }

            return true;
        }
        [HttpGet]
        public ActionResult PotvrdaRegistracijeJson(int id, string guid)
        {

            masterEntities db = new masterEntities();
            user u=db.user.Find(id);
            u.confirmation_key = "Aktivan";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
