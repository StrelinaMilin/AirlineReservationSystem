using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationSystem.Controllers
{
    public class SignupController : Controller
    {
        private readonly DBAirlineReservationContext db;
        public SignupController(DBAirlineReservationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(TblUser tblUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.TblUsers.Add(tblUser);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Signup");
                }
                return View();
            }
            catch
            {
                ViewBag.msg = "This Account Already Exits";
                return View();
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
            TblUser tblUser = new TblUser();
            TempData["Loginlist"] = login.Emailid;
            TempData.Keep();
            var uname = (from user in db.TblUsers
                       where user.Emailid == login.Emailid
                       select user.FirstName).FirstOrDefault();
            TempData["Name"] = uname;
            TempData.Keep();
            int authorised_user = (from user in db.TblUsers where login.Emailid == user.Emailid && login.Password == user.Password select user.Userid).FirstOrDefault();
            tblUser = db.TblUsers.Find(authorised_user);
            if (ModelState.IsValid)
            {
                if (tblUser != null)
                {
                    
                    return RedirectToAction("ReservedFlight", "User");
                }
                else
                {
                    string authorised_admin = (from admin in db.TblAdmins where login.Emailid == admin.Adminemail && login.Password == admin.AdminPassword select admin.Adminemail).FirstOrDefault();
                    if (authorised_admin != null)
                    {
                        return RedirectToAction("DisplayFlight", "Admin");
                    }
                }
                ViewBag.msg = "Incorrect Id or Password";
                return View();
                
            }
            return View();
        }
       


    }
}

