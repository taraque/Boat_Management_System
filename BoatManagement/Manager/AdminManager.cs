using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoatManagement.Gateway;
using BoatManagement.Models;

namespace BoatManagement.Manager
{
    public class AdminManager
    {
        AdminGateway aAdminGateway = new AdminGateway();
        UserGateway aUserGateway = new UserGateway();
        public BoatInfo GetBoatInfo(int id)
        {
            return aAdminGateway.GetBoatInfo(id);
        }

        public List<Sailors> GetSailors(int id)
        {
            return aAdminGateway.GetSailors(id);
        }

        public List<Fishermans> GetFishermans(int id)
        {
            return aAdminGateway.GetFishermans(id);
        }

        public string UpdateBoatInfo(BoatInfo boatInfo)
        {
            int rowAffected = aAdminGateway.UpdateBoatInfo(boatInfo);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Boat information updating failed. Please try again!";
            }
        }

        public List<BoatInfo> GetReturnedBoatInfo()
        {
            return aAdminGateway.GetReturnedBoatInfo();
        }
    }
}