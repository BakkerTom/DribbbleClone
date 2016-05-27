using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace Dribbble.Models
{
    public static class SQL
    {
        private static OracleConnection _connection;
        public static bool OpenConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"];
            _connection = new OracleConnection(connectionString.ToString());

            if (_connection != null)
            {
                _connection.Open();

                if (Connection.State == ConnectionState.Open)
                {
                    return true;
                }
                else return false;

            }
            else
            {
                return false;
            }
        }

        public static OracleConnection Connection {
            get { return _connection; } 
        }
    }
}