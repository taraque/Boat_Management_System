using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoatManagement.Manager;
using BoatManagement.Models;

namespace BoatManagement.Controllers
{
    [Authorize(Roles = "Volunteer")]
    public class VolunteerController : Controller
    {
        UserManager aUserManager = new UserManager();
        AdminManager aAdminManager = new AdminManager();
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult AddBoat()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBoat(BoatInfo boatInfo)
        {
            int totalSailor = boatInfo.Sailors.Count;
            int totalFisherman = boatInfo.Fishermans.Count;
            boatInfo.TotalPerson = totalSailor + totalFisherman;
            boatInfo.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            boatInfo.FinishDate = DateTime.Now.AddDays(12).ToLongDateString();
            boatInfo.Status = false;
            string message = aUserManager.SaveBoat(boatInfo);
            if (message == "Success")
            {
                ViewBag.Message = "Boat information saved successfully";
            }
            else
            {
                ViewBag.ErrorMessage = message;
            }
            return View();
        }

        public ActionResult CurrentBoats()
        {
            ViewBag.Message = TempData["Message"];
            List<BoatInfo> boatInfos = aUserManager.GetAllBoatInfo();
            ViewBag.GetAllBoatInfo = boatInfos;
            return View();
        }

        public ActionResult ChangeBoatStatus(int id)
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
            return RedirectToAction("CurrentBoats", "Volunteer");
        }
        public ActionResult ReturnedBoats()
        {
            List<BoatInfo> boatInfos = aAdminManager.GetReturnedBoatInfo();
            ViewBag.GetReturnedBoats = boatInfos;
            return View();
        }
    }
}