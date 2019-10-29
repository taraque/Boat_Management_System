using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoatManagement.Manager;
using BoatManagement.Models;

namespace BoatManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        UserManager aUserManager = new UserManager();
        AdminManager aAdminManager = new AdminManager();
        public ActionResult Home()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.Message = TempData["Message"];
                List<BoatInfo> boatInfos = aUserManager.GetAllBoatInfo();
                ViewBag.GetAllBoatInfo = boatInfos;
            }
            return View();
        }
        public ActionResult ChangeBoatStatus(int id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id > 0)
                {
                    string finishDate = DateTime.Now.ToLongDateString();
                    string message = aUserManager.ChangeBoatStatus(id, finishDate);
                    if (message == "Success")
                    {
                        TempData["Message"] = "The boat reached and status updated successfully.";
                    }
                }
            }
            return RedirectToAction("Home", "Admin");
        }

        public ActionResult BoatDetails(int id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id > 0)
                {
                    BoatInfo boatInfo = aAdminManager.GetBoatInfo(id);
                    List<Sailors> sailors = aAdminManager.GetSailors(id);
                    List<Fishermans> fishermans = aAdminManager.GetFishermans(id);
                    ViewBag.GetBoatInfo = boatInfo;
                    ViewBag.GetAllSailors = sailors;
                    ViewBag.GetAllFisherman = fishermans;
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BoatDetails(BoatInfo boatInfo)
        {
            if (User.IsInRole("Admin"))
            {
                string message = aAdminManager.UpdateBoatInfo(boatInfo);
                if (message == "Success")
                {
                    ViewBag.Message = "Boat information updated successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
                BoatInfo aBoatInfo = aAdminManager.GetBoatInfo(boatInfo.BoatId);
                List<Sailors> sailors = aAdminManager.GetSailors(boatInfo.BoatId);
                List<Fishermans> fishermans = aAdminManager.GetFishermans(boatInfo.BoatId);
                ViewBag.GetBoatInfo = aBoatInfo;
                ViewBag.GetAllSailors = sailors;
                ViewBag.GetAllFisherman = fishermans;
            }
            return View();
        }

        public ActionResult ReturnedBoats()
        {
            List<BoatInfo> boatInfos = aAdminManager.GetReturnedBoatInfo();
            ViewBag.GetReturnedBoats = boatInfos;
            return View();
        }
    }
}