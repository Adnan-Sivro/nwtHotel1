


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
                        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                            return View();
                        else return RedirectToAction("Login3", "Home");
                    }
                    
                    public ActionResult Contact()
                    {
                        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                            return View("Contact");
                        else return RedirectToAction("Login3", "Home");
                    }

                    public ActionResult FoodOrder()
                    {
                        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                            return View();
                        else return RedirectToAction("Login3", "Home");
                    }

                    public ActionResult ReservationAdmin()
                    {
                        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                            return View();
                        else return RedirectToAction("Login3", "Home");
                    }

                    public ActionResult FoodOrderAdmin()
                    {
                        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                            return View();
                        else return RedirectToAction("Login3", "Home");
                    }
                    public ActionResult Images(int id = 1)
                    {
                        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            using (masterEntities contextObj = new masterEntities())
                            {
                                var getAllImage = contextObj.image.ToList();
                                return View(getAllImage);
                            }
                        }
                        else return RedirectToAction("Login3", "Home");

                    }

                }

            }