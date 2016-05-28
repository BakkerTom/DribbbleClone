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
        public List<Shot> GetAll()
        {
            List<Shot> Shots = new List<Shot>();

            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM Shot", SQL.Connection);
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