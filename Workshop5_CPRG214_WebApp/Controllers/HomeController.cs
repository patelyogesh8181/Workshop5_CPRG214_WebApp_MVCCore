using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop5_CPRG214_WebApp.Models;

namespace Workshop5_CPRG214_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TravelExpertDBContext _context;

        public HomeController(TravelExpertDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag._UserName = "";
            ViewBag._MessageNo = string.Empty;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("_Email");
            HttpContext.Session.Remove("_Password");
            HttpContext.Session.Remove("_CustomerId");
            HttpContext.Session.Remove("_FirstName");
            HttpContext.Session.Remove("_LastName");

            HttpContext.Session.Clear();

            return View("Index");
        }

        public ActionResult Create(Customer objCustomer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(objCustomer);
                    _context.SaveChanges();
                    //db.Customers.Add(objCustomer);
                    ViewBag._MessageNo = "1";
                }
                else
                {
                    ViewBag._MessageNo = "0";
                }
            }
            catch (Exception ex)
            {
                ViewBag._MessageNo = "-1";
            }
            return View("Register");
        }

        public ActionResult CheckLogin([Bind("CustEmail,PasswordNotHashed")] Customer objcustomer)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                IQueryable<Customer> myUsers = from user in _context.Customers
                                               where user.CustEmail == objcustomer.CustEmail && user.PasswordNotHashed == objcustomer.PasswordNotHashed
                                               select user;
                Customer myCustomer = myUsers.FirstOrDefault();

                if (myCustomer != null)
                {
                    HttpContext.Session.SetString("_Email", myCustomer.CustEmail);
                    HttpContext.Session.SetString("_Password", myCustomer.PasswordNotHashed);
                    HttpContext.Session.SetString("_CustomerId", Convert.ToString(myCustomer.CustomerId));
                    HttpContext.Session.SetString("_FirstName", myCustomer.CustFirstName);
                    HttpContext.Session.SetString("_LastName", myCustomer.CustLastName);
                    HttpContext.Session.SetString("_UserName", myCustomer.CustFirstName + " "+ myCustomer.CustLastName);

                    ViewBag._FirstName = HttpContext.Session.GetString("_FirstName");
                    ViewBag._LastName = HttpContext.Session.GetString("_LastName");
                    ViewBag._UserName = HttpContext.Session.GetString("_FirstName") + " " + HttpContext.Session.GetString("_LastName");
                    return View("Index");
                }
                else
                {
                    ViewBag._MessageNo = "0";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag._MessageNo = "-1";
                return View("Login");
            }
        }

        //public ActionResult Packages() {
        //    var lst = _context.Packages.ToList();
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
