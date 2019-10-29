using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BoatManagement.Models;

namespace BoatManagement.Gateway
{
    public class UserGateway : ParentGateway
    {
        public string GetVolunteerRole(string username)
        {
            Query = "SELECT Role FROM Volunteer WHERE Username = @username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Connection.Open();
            Reader = Command.ExecuteReader();
            string role = "";
            if (Reader.Read())
            {
                role = Reader["Role"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return role;
        }

        public string GetAdminRole(string username)
        {
            Query = "SELECT Role FROM Admin WHERE Username = @username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Connection.Open();
            Reader = Command.ExecuteReader();
            string role = "";
            if (Reader.Read())
            {
                role = Reader["Role"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return role;
        }

        public bool IsVolunteerLoginValid(LoginModel aLoginModel)
        {
            Query = "SELECT Username, Password FROM Volunteer WHERE Username = @username AND Password = @password";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", aLoginModel.Username);
            Command.Parameters.AddWithValue("password", aLoginModel.Password);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }
        public bool IsAdminLoginValid(LoginModel aLoginModel)
        {
            Query = "SELECT Username, Password FROM Admin WHERE Username = @username AND Password = @password";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", aLoginModel.Username);
            Command.Parameters.AddWithValue("password", aLoginModel.Password);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public bool IsBoatValid(string boatName, string boatRegNo)
        {
            Query = "SELECT BoatName,BoatRegNo  FROM BoatInfo WHERE BoatName = @boatname OR BoatRegNo = @boatRegNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("boatname", boatName);
            Command.Parameters.AddWithValue("boatRegNo", boatRegNo);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public bool IsOwnerValid(string ownerNidNo)
        {
            Query = "SELECT OwnerNidNo FROM BoatInfo WHERE OwnerNidNo = @ownerNidNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("ownerNidNo", ownerNidNo);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public bool IsMobileNoExist(string ownerMobileNo)
        {
            Query = "SELECT OwnerMobileNo FROM BoatInfo WHERE OwnerMobileNo = @ownerMobileNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("ownerMobileNo", ownerMobileNo);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int SaveBoat(BoatInfo boatInfo)
        {
            Query = "INSERT INTO BoatInfo VALUES(@boatName, @boatRegNo, @ownerName, @ownerNidNo, @ownerMobileNo, @startingTime, @startDate, @finishDate, @totalPerson, @status)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("boatName", boatInfo.BoatName);
            Command.Parameters.AddWithValue("boatRegNo", boatInfo.BoatRegNo);
            Command.Parameters.AddWithValue("ownerName", boatInfo.OwnerName);
            Command.Parameters.AddWithValue("ownerNidNo", boatInfo.OwnerNidNo);
            Command.Parameters.AddWithValue("ownerMobileNo", boatInfo.OwnerMobileNo);
            Command.Parameters.AddWithValue("startingTime", boatInfo.StartingTime);
            Command.Parameters.AddWithValue("startDate", boatInfo.StartDate);
            Command.Parameters.AddWithValue("finishDate", boatInfo.FinishDate);
            Command.Parameters.AddWithValue("totalPerson", boatInfo.TotalPerson);
            Command.Parameters.AddWithValue("status", boatInfo.Status);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            if (rowAffected > 0)
            {
                int rowAffectedSailors = SaveSailors(boatInfo.BoatRegNo, boatInfo.Sailors);
                int rowAffectedFishermans = SaveFishermans(boatInfo.BoatRegNo, boatInfo.Fishermans);
                if (rowAffectedSailors <= 0 || rowAffectedFishermans <= 0)
                {
                    rowAffected = 0;
                }
            }
            return rowAffected;
        }

        private int SaveFishermans(string boatRegNo, List<Fishermans> boatInfoFishermans)
        {
            int rowAffected = 0;
            int boatId = GetBoatId(boatRegNo);
            if (boatId > 0)
            {
                foreach (Fishermans fisherman in boatInfoFishermans)
                {
                    Query = "INSERT INTO Fishermans VALUES(@fishermanName, @fishermanMobileNo,@boatId)";
                    Command = new SqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("fishermanName", fisherman.FishermanName);
                    Command.Parameters.AddWithValue("fishermanMobileNo", fisherman.FishermanMobileNo);
                    Command.Parameters.AddWithValue("boatId", boatId);
                    Connection.Open();
                    rowAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
            return rowAffected;
        }

        private int GetBoatId(string boatRegNo)
        {
            Query = "SELECT BoatId FROM BoatInfo WHERE BoatRegNo = @boatRegNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("boatRegNo", boatRegNo);
            Connection.Open();
            Reader = Command.ExecuteReader();
            int boatId = 0;
            if (Reader.Read())
            {
                boatId = Convert.ToInt32(Reader["BoatId"]);
            }
            Reader.Close();
            Connection.Close();
            return boatId;
        }

        private int SaveSailors(string boatRegNo, List<Sailors> boatInfoSailors)
        {
            int rowAffected = 0;
            int boatId = GetBoatId(boatRegNo);
            if (boatId > 0)
            {
                foreach (Sailors sailor in boatInfoSailors)
                {
                    Query = "INSERT INTO Sailors VALUES(@sailorName, @sailorMobileNo,@boatId)";
                    Command = new SqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("sailorName", sailor.SailorName);
                    Command.Parameters.AddWithValue("sailorMobileNo", sailor.SailorMobileNo);
                    Command.Parameters.AddWithValue("boatId", boatId);
                    Connection.Open();
                    rowAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
            return rowAffected;
        }

        public List<BoatInfo> GetAllBoatInfo()
        {
            Query = "SELECT * FROM BoatInfo WHERE Status = 'False'";
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
        
        public int ChangeBoatStatus(int id, string finishDate)
        {
            Query = "UPDATE BoatInfo SET FinishDate = @finishDate, Status = 'True' WHERE BoatId = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Command.Parameters.AddWithValue("finishDate", finishDate);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}