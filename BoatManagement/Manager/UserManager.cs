using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoatManagement.Gateway;
using BoatManagement.Models;

namespace BoatManagement.Manager
{
    public class UserManager
    {
        UserGateway aUserGateway = new UserGateway();
        public string GetRole(string username)
        {
            string role = aUserGateway.GetVolunteerRole(username);
            if (role == "")
            {
                role = aUserGateway.GetAdminRole(username);
                return role;
            }
            return role;
        }

        public bool IsVolunteerLoginValid(LoginModel aLoginModel)
        {
            return aUserGateway.IsVolunteerLoginValid(aLoginModel);
        }
        public bool IsAdminLoginValid(LoginModel aLoginModel)
        {
            return aUserGateway.IsAdminLoginValid(aLoginModel);
        }

        public string SaveBoat(BoatInfo boatInfo)
        {
            if (aUserGateway.IsBoatValid(boatInfo.BoatName, boatInfo.BoatRegNo) == false)
            {
                int rowAffected = aUserGateway.SaveBoat(boatInfo);
                if (rowAffected > 0)
                {
                    return "Success";
                }
                else
                {
                    return "Boat information saving failed. Try again!";
                }
            }
            return "The boat is already exist";
        }

        public List<BoatInfo> GetAllBoatInfo()
        {
            return aUserGateway.GetAllBoatInfo();
        }

        public string ChangeBoatStatus(int id, string finishDate)
        {
            int rowAffected = aUserGateway.ChangeBoatStatus(id, finishDate);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Boat status updating failed. Try again!";
            }
        }
    }
}