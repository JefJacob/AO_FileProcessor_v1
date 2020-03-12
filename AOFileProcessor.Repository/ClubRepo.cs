using System;
using System.Collections.Generic;
using System.Text;  
using AOFileProcessor.Entities;
using System.Data.SqlClient;
using NLog;
using System.Configuration;

namespace AOFileProcessor.Repository
{ 
    public static class ClubRepo
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static int AddClub(ClubEntity club)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string insertStatement =
                "INSERT into Clubs " +
                "(ClubCode,Name,ShortName) " +
                "VALUES (@ClubCode,@Name,@ShortName)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);

            insertCommand.Parameters.AddWithValue(
                "@ClubCode", club.ClubCode);
            insertCommand.Parameters.AddWithValue(
                "@Name", club.Name);
            insertCommand.Parameters.AddWithValue(
                "@ShortName", club.ShortName);


            try
            {
                connection.Open();
                int value = insertCommand.ExecuteNonQuery();
                return value;

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                    logger.Error("Duplicate:" + club.ClubCode);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static ClubEntity GetClub(String ClubCode)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string selectStatement
                = "SELECT * "

                + "FROM Clubs "
                + "WHERE ClubCode = @Code";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@Code", ClubCode);
            try
            {
                connection.Open();
                SqlDataReader proReader =
                    selectCommand.ExecuteReader(
                        System.Data.CommandBehavior.SingleRow);
                if (proReader.Read())
                {
                    ClubEntity club = new ClubEntity();
                    club.ClubCode = proReader["ClubCode"].ToString();
                    club.Name = proReader["Name"].ToString();
                    club.ShortName = proReader["ShortName"].ToString();


                    return club;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
