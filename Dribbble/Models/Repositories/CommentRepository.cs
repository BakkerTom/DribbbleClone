using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace Dribbble.Models.Repositories
{
    public class CommentRepository
    {
        /// <summary>
        /// Bekijk de comments voor een bepaald shot
        /// </summary>
        /// <param name="ShotID"></param>
        /// <returns>Lijst met comments per shot</returns>
        public List<Comment> CommentsForShot(int ShotID)
        {
            List<Comment> comments = new List<Comment>();

            if (SQL.OpenConnection())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "SELECT * FROM Comm WHERE SHOTID = :ShotID";
                cmd.Connection = SQL.Connection;

                cmd.Parameters.Add("ShotID", ShotID);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comments.Add(CommentFromReader(dr));
                }
                
            }

            return comments;
        }

        private Comment CommentFromReader(OracleDataReader dr)
        {
            Comment c = new Comment();

            c.ID = Convert.ToInt32(dr["ID"]);
            c.ShotID = Convert.ToInt32(dr["ShotID"]);
            c.AccountID = Convert.ToInt32(dr["AccountId"]);
            c.Message = dr["Message"].ToString();

            return c;
        }
    }
}