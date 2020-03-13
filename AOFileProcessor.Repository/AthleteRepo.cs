using System;
using System.Collections.Generic;
using System.Text;
using AOFileProcessor.Entities;
using System.Data.SqlClient;
using System.Configuration;
using NLog;

namespace AOFileProcessor.Repository
{
    public class AthleteRepo
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static int AddAthlete(AthleteEntity athlete)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string insertStatement =
                "INSERT into Athletes " +
                "(ACNum,	FirstName,	LastName,	DOB,	AthleteGender,	ClubCode,	Address,	City,	Phone,	AthleteEmail,	HeadShot,		ClubAffiliationSince) " +
                "VALUES (	@ACNum,	@FirstName,	@LastName,	@DOB,	@AthleteGender,	@ClubCode,	@Address,	@City,	@Phone,	@AthleteEmail,	@HeadShot,		@ClubAffiliationSince)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);

            //insertCommand.Parameters.AddWithValue("@AthleteId", athlete.AthleteId);
            insertCommand.Parameters.AddWithValue("@ACNum", athlete.ACNum);
            insertCommand.Parameters.AddWithValue("@FirstName", athlete.FirstName);
            insertCommand.Parameters.AddWithValue("@LastName", athlete.LastName);
            insertCommand.Parameters.AddWithValue("@DOB", athlete.DOB);
            insertCommand.Parameters.AddWithValue("@AthleteGender", athlete.AthleteGender);
            insertCommand.Parameters.AddWithValue("@ClubCode", athlete.ClubCode);
            insertCommand.Parameters.AddWithValue("@Address", athlete.Address);
            insertCommand.Parameters.AddWithValue("@City", athlete.City);
            insertCommand.Parameters.AddWithValue("@Phone", athlete.Phone);
            insertCommand.Parameters.AddWithValue("@AthleteEmail", athlete.AthleteEmail);
            insertCommand.Parameters.AddWithValue("@HeadShot", athlete.HeadShot);
            //insertCommand.Parameters.AddWithValue("@AthleteSpecialNoteId", athlete.AthleteSpecialNoteId);
            insertCommand.Parameters.AddWithValue("@ClubAffiliationSince", athlete.ClubAffiliationSince);



            try
            {
                connection.Open();
                int value = insertCommand.ExecuteNonQuery();
                return value;

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                    Console.WriteLine("Duplicate Athlete: "+athlete.FirstName);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static AthleteEntity GetAthleteByACNum(String ACNum)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string selectStatement
                = "SELECT * "
                + "FROM AThletes "
                + "WHERE ACNum = @ACNum";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@ACNum", ACNum);
            try
            {
                connection.Open();
                SqlDataReader proReader =
                    selectCommand.ExecuteReader(
                        System.Data.CommandBehavior.SingleRow);
                if (proReader.Read())
                {
                    AthleteEntity athlete = new AthleteEntity();
                    athlete.AthleteId = Convert.ToInt32(proReader["AthleteId"]);
                    athlete.ACNum = proReader["ACNum"].ToString();
                    athlete.FirstName = proReader["FirstName"].ToString();
                    athlete.LastName = proReader["LastName"].ToString();
                    athlete.DOB = Convert.ToDateTime(proReader["DOB"]);
                    athlete.AthleteGender = proReader["AthleteGender"].ToString();
                    athlete.ClubCode = proReader["ClubCode"].ToString();
                    athlete.Address = proReader["Address"].ToString();
                    athlete.City = proReader["City"].ToString();
                    athlete.Phone = proReader["Phone"].ToString();
                    athlete.AthleteEmail = proReader["AthleteEmail"].ToString();
                    athlete.HeadShot = proReader["HeadShot"].ToString();
                    athlete.AthleteSpecialNoteId = Convert.ToInt32(proReader["AthleteSpecialNoteId"]);
                    athlete.ClubAffiliationSince = Convert.ToDateTime(proReader["ClubAffiliationSince"]);
                    
                    return athlete;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                logger.Error("Exception : " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int GetAthleteIdByACNum(String ACNum)
        {
            if (String.IsNullOrWhiteSpace(ACNum))
                return 0;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string selectStatement
                = "SELECT * "
                + "FROM AThletes "
                + "WHERE ACNum = @ACNum";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@ACNum", ACNum);
            try
            {
                connection.Open();
                SqlDataReader proReader =
                    selectCommand.ExecuteReader(
                        System.Data.CommandBehavior.SingleRow);
                if (proReader.Read())
                {
                    
                    int athleteId = Convert.ToInt32(proReader["AthleteId"]);
                    
                    return athleteId;
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException ex)
            {
                logger.Error("Exception : " + ex.Message);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        public static int GetAthleteIdByName(String fName, String lName, DateTime dob)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string selectStatement
                = "SELECT * "
                + "FROM AThletes "
                + "WHERE FirstName+LastName+CONVERT(VARCHAR(10),DOB,105) = @FirstName+@LastName+@DOB";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@FirstName", fName);
            selectCommand.Parameters.AddWithValue(
               "@LastName", lName);
            selectCommand.Parameters.AddWithValue(
               "@DOB", dob.ToString("dd-MM-yyyy"));
            try
            {
                connection.Open();
                SqlDataReader proReader =
                    selectCommand.ExecuteReader(
                        System.Data.CommandBehavior.SingleRow);
                if (proReader.Read())
                {

                    int athleteId = Convert.ToInt32(proReader["AthleteId"]);

                    return athleteId;
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException ex)
            {
                logger.Error("Exception : " + ex.Message);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static string GetAthleteNameById(int athleteId)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string selectStatement
                = "SELECT * "
                + "FROM AThletes "
                + "WHERE AthleteId=@AthleteId";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@AthleteId", athleteId);
           
            try
            {
                connection.Open();
                SqlDataReader proReader =
                    selectCommand.ExecuteReader(
                        System.Data.CommandBehavior.SingleRow);
                if (proReader.Read())
                {

                    string athleteName =proReader["FirstName"].ToString()+" "+ proReader["LastName"].ToString()+" "+ proReader["DOB"].ToString();

                    return athleteName;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                logger.Error("Exception : " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
