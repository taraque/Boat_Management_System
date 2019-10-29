using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BoatManagement.Manager;
using BoatManagement.Models;

namespace BoatManagement.Controllers
{
    public class AccountController : Controller
    {
        UserManager aUserManager = new UserManager();
        public ActionResult Login()
        {
            if (User.IsInRole("Volunteer"))
            {
                return RedirectToAction("Home", "Volunteer");
            }
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Home", "Admin");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel aLoginModel, string returnUrl ="")
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "Account");
            }
            if (aUserManager.IsVolunteerLoginValid(aLoginModel))
            {
                int timeout = aLoginModel.RememberMe ? 10080 : 40;
                var ticket = new FormsAuthenticationTicket(aLoginModel.Username, aLoginModel.RememberMe, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Home","Volunteer");
                }
            }
            if (aUserManager.IsAdminLoginValid(aLoginModel))
            {
                int timeout = aLoginModel.RememberMe ? 10080 : 40;
                var ticket = new FormsAuthenticationTicket(aLoginModel.Username, aLoginModel.RememberMe, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Home", "Admin");
                }
            }
            ViewBag.ErrorMessage = "Your username or password is incorrect. Please try again";
            return View();
        }

        [Authorize(Roles = "Volunteer")]
        public ActionResult VolunteerLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}