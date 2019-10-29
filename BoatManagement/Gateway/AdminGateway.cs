using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BoatManagement.Models;

namespace BoatManagement.Gateway
{
    public class AdminGateway : ParentGateway
    {
        public BoatInfo GetBoatInfo(int id)
        {
            Query = "SELECT * FROM BoatInfo WHERE BoatId = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            BoatInfo boatInfo = null;
            while (Reader.Read())
            {
                boatInfo = new BoatInfo();
                boatInfo.BoatId = Convert.ToInt32(Reader["BoatId"]);
                boatInfo.BoatName = Reader["BoatName"].ToString();
                boatInfo.BoatRegNo = Reader["BoatRegNo"].ToString();
                boatInfo.OwnerName = Reader["OwnerName"].ToString();
                boatInfo.OwnerNidNo = Reader["OwnerNidNo"].ToString();
                boatInfo.OwnerMobileNo = Reader["OwnerMobileNo"].ToString();
                boatInfo.StartingTime = Reader["StartingTime"].ToString();
                boatInfo.StartDate = Reader["StartDate"].ToString();
                boatInfo.FinishDate = Reader["FinishDate"].ToString();
                boatInfo.TotalPerson = Convert.ToInt32(Reader["TotalPerson"]);
                boatInfo.Status = Convert.ToBoolean(Reader["Status"]);
            }
            Reader.Close();
            Connection.Close();
            return boatInfo;
        }

        public List<Sailors> GetSailors(int id)
        {
            Query = "SELECT * FROM Sailors WHERE BoatId = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Sailors> sailors = new List<Sailors>();
            while (Reader.Read())
            {
                Sailors sailor = new Sailors();
                sailor.BoatId = Convert.ToInt32(Reader["BoatId"]);
                sailor.SailorName = Reader["SailorName"].ToString();
                sailor.SailorMobileNo = Reader["SailorMobileNo"].ToString();
                sailor.SailorId = Convert.ToInt32(Reader["SailorId"]);
                sailors.Add(sailor);
            }
            Reader.Close();
            Connection.Close();
            return sailors;
        }

        public List<Fishermans> GetFishermans(int id)
        {
            Query = "SELECT * FROM Fishermans WHERE BoatId = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Fishermans> fishermans = new List<Fishermans>();
            while (Reader.Read())
            {
                Fishermans fisherman = new Fishermans();
                fisherman.BoatId = Convert.ToInt32(Reader["BoatId"]);
                fisherman.FishermanName = Reader["FishermanName"].ToString();
                fisherman.FishermanMobileNo = Reader["FishermanMobileNo"].ToString();
                fisherman.FishermanId = Convert.ToInt32(Reader["FishermanId"]);
                fishermans.Add(fisherman);
            }
            Reader.Close();
            Connection.Close();
            return fishermans;
        }

        public int UpdateBoatInfo(BoatInfo boatInfo)
        {
            Query = "UPDATE BoatInfo SET OwnerName = @ownerName, OwnerNidNo = @ownerNidNo, OwnerMobileNo = @ownerMobileNo, StartDate = @startDate, StartingTime = @startingTime WHERE BoatId = @boatId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("ownerName", boatInfo.OwnerName);
            Command.Parameters.AddWithValue("ownerNidNo", boatInfo.OwnerNidNo);
            Command.Parameters.AddWithValue("ownerMobileNo", boatInfo.OwnerMobileNo);
            Command.Parameters.AddWithValue("startDate", boatInfo.StartDate);
            Command.Parameters.AddWithValue("startingTime", boatInfo.StartingTime);
            Command.Parameters.AddWithValue("boatId", boatInfo.BoatId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<BoatInfo> GetReturnedBoatInfo()
        {
            Query = "SELECT * FROM BoatInfo WHERE Status = 'True'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<BoatInfo> boatInfos = new List<BoatInfo>();
            while (Reader.Read())
            {
                BoatInfo boatInfo = new BoatInfo();
                boatInfo.BoatId = Convert.ToInt32(Reader["BoatId"]);
                boatInfo.BoatName = Reader["BoatName"].ToString();
                boatInfo.BoatRegNo = Reader["BoatRegNo"].ToString();
                boatInfo.OwnerName = Reader["OwnerName"].ToString();
                boatInfo.OwnerNidNo = Reader["OwnerNidNo"].ToString();
                boatInfo.OwnerMobileNo = Reader["OwnerMobileNo"].ToString();
                boatInfo.StartingTime = Reader["StartingTime"].ToString();
                boatInfo.StartDate = Reader["StartDate"].ToString();
                boatInfo.FinishDate = Reader["FinishDate"].ToString();
                boatInfo.TotalPerson = Convert.ToInt32(Reader["TotalPerson"]);
                boatInfo.Status = Convert.ToBoolean(Reader["Status"]);
                boatInfos.Add(boatInfo);
            }
            Reader.Close();
            Connection.Close();
            return boatInfos;
        }
    }
}