using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;

namespace Dribbble.Models.Repositories
{
    public class AccountRepository
    {
        public Account getByID(int id)
        {
            Account a = new Account();

            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = SQL.Connection;
                cmd.CommandText = "SELECT * FROM Account WHERE ID = " + id;

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    a = convertAccount(dr);
                }
            }

            return a;
        }

        public Account getByAccountName(string name)
        {
            Account a = new Account();

            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = SQL.Connection;
                cmd.CommandText = "SELECT * FROM Account WHERE AccountName = :name";
                cmd.Parameters.Add("name", name);

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    a = convertAccount(dr);
                }
            }

            return a;
        }


        /// <summary>
        /// Checks if a username/password combination exists in the datatabase
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>True if user exists and password is correct.</returns>
        public bool isValid(string username, string password)
        {
            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = SQL.Connection;
                cmd.CommandText = "SELECT Accountname FROM Account WHERE Accountname = '" + username + "' AND Password = '" + password + "'";

                OracleDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void Insert(Account a)
        {
            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText =
                    "INSERT INTO Account(Name, Accountname, Email, Password, Location, Website, Bio, AccountType) " +
                    "VALUES (:Name, :Accountname, :Email, :Password, :Location, :Website, :Bio, 'PLAYER')";
                cmd.Connection = SQL.Connection;

                cmd.Parameters.Add("Name", a.Name);
                cmd.Parameters.Add("Accountname", a.AccountName);
                cmd.Parameters.Add("Email", a.Email);
                cmd.Parameters.Add("Password", a.Password);
                cmd.Parameters.Add("Location", a.Location);
                cmd.Parameters.Add("Website", a.Website);
                cmd.Parameters.Add("Bio", a.Bio);

                cmd.ExecuteReader();
            }
        }

        private Account convertAccount(OracleDataReader dr)
        {
            Account a = new Account();

            a.ID = Convert.ToInt32(dr["ID"]);
            a.Name = dr["Name"].ToString();
            a.AccountName = dr["AccountName"].ToString();
            a.Email = dr["Email"].ToString();
            a.Bio = dr["Bio"].ToString();
            a.Location = dr["Location"].ToString();
            return a;
        }
    }
}