


using System;
            using System.Collections.Generic;
            using System.Linq;
            using System.Web;
            using System.Web.Mvc;
 
            namespace HotelNWT.Controllers
            {
                public class HomeController : Controller
                {
                    //
                    // GET: /Home/        
                    public ActionResult Index()
                    {
                        return View();
                    }

                    public ActionResult Login3() // Login from Database
                    {
                        return View();
                    }

                    public ActionResult Registration() // Implement Cascade dropdownlist
                    {
                        return View();
                    }

                    public ActionResult Reservation() // Implement Cascade dropdownlist
                    {
                        return View();
                    }
                    
                    public ActionResult Contact()
                    {
                        return View("Contact");
                    }

                    public ActionResult FoodOrder()
                    {
                        return View();
                    }

                    public ActionResult ReservationAdmin()
                    {
                        return View();
                    }

                }

            }