using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace Dribbble.Models
{
    public class ShotRepository
    {
        /// <summary>
        /// Haal alle shots uit de database
        /// </summary>
        /// <returns>List van Shot op chronologische volgorde</returns>
        public List<Shot> GetAll()
        {
            List<Shot> Shots = new List<Shot>();

            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM Shot ORDER BY createdAt DESC", SQL.Connection);
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Shots.Add(convertShot(dr));
                    }
                }

            }

            SQL.Connection.Close();

            return Shots;
        }

        /// <summary>
        /// Haal een shot object met een bepaald ID op
        /// </summary>
        /// <param name="id">Shot ID</param>
        /// <returns>Shot</returns>
        public Shot GetByID(int id)
        {
            Shot s = new Shot();

            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = SQL.Connection;
                cmd.CommandText = "SELECT * FROM Shot WHERE ID = " + id;

                OracleDataReader dr = cmd.ExecuteReader();
       
                if (dr.Read())
                {
                    s = convertShot(dr);
                }

            }

            return s;
        }

        /// <summary>
        /// Insert een nieuw shot in de Database
        /// </summary>
        /// <param name="s">Het shot object</param>
        public void Insert(Shot s)
        {
            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = SQL.Connection;
                cmd.CommandText =
                    "INSERT INTO Shot(AccountID, Title, Description, ImageURL) VALUES (:account, :title, :description, :imagurl)";

                cmd.Parameters.Add("account", s.AccountID);
                cmd.Parameters.Add("title", s.Title);
                cmd.Parameters.Add("description", s.Description);
                cmd.Parameters.Add("imageurl", s.ImageURL);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Het aantal likes voor een bepaald shot
        /// </summary>
        /// <param name="ShotID">ShotID</param>
        /// <returns>Int aantal likes</returns>
        public int getLikes(int ShotID)
        {
            int likes = 0;

            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "SELECT COUNT(*) AS LIKES FROM Love WHERE ShotID = :ShotID";
                cmd.Connection = SQL.Connection;
                cmd.Parameters.Add("ShotID", ShotID);

                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        likes = Convert.ToInt32(dr["Likes"]);
                    }
                }

            }

            return likes;
        }

        /// <summary>
        /// Converteerd een OracleDataReader van de Shot tabel naar een Shot object
        /// </summary>
        /// <param name="dr"></param>
        /// <returns>Shot Object</returns>
        private Shot convertShot(OracleDataReader dr)
        {
            Shot s = new Shot();

            s.ID = Convert.ToInt32(dr["ID"]);
            s.AccountID = Convert.ToInt32(dr["AccountID"]);
            s.Title = dr["Title"].ToString();
            s.Description = dr["Description"].ToString();
            s.ImageURL = dr["ImageURL"].ToString();
            string dateString = dr["CreatedAt"].ToString();
            s.CreationDate = DateTime.Parse(dateString);
            return s;
        }
    }
}