using System;
using System.Collections.Generic;
using System.Text;
using AOFileProcessor.Entities;
using System.Data.SqlClient;

namespace AOFileProcessor.Repository
{
    public class AthleteRepo
    {
        public static int AddAthlete(AthleteEntity athlete)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VAIO;Initial catalog=AO_TESTDB_V7;Integrated Security=True");
            string insertStatement =
                "INSERT into Athletes " +
                "(ACNum,	Fname,	Lname,	DOB,	Gender,	ClubCode,	Address,	City,	Phone,	Email,	HeadShot,		ClubAffiliationSince) " +
                "VALUES (	@ACNum,	@Fname,	@Lname,	@DOB,	@Gender,	@ClubCode,	@Address,	@City,	@Phone,	@Email,	@HeadShot,		@ClubAffiliationSince)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);

            //insertCommand.Parameters.AddWithValue("@AthleteId", athlete.AthleteId);
            insertCommand.Parameters.AddWithValue("@ACNum", athlete.ACNum);
            insertCommand.Parameters.AddWithValue("@Fname", athlete.Fname);
            insertCommand.Parameters.AddWithValue("@Lname", athlete.Lname);
            insertCommand.Parameters.AddWithValue("@DOB", athlete.DOB);
            insertCommand.Parameters.AddWithValue("@Gender", athlete.Gender);
            insertCommand.Parameters.AddWithValue("@ClubCode", athlete.ClubCode);
            insertCommand.Parameters.AddWithValue("@Address", athlete.Address);
            insertCommand.Parameters.AddWithValue("@City", athlete.City);
            insertCommand.Parameters.AddWithValue("@Phone", athlete.Phone);
            insertCommand.Parameters.AddWithValue("@Email", athlete.Email);
            insertCommand.Parameters.AddWithValue("@HeadShot", athlete.HeadShot);
            //insertCommand.Parameters.AddWithValue("@AthleteSpecialNoteid", athlete.AthleteSpecialNoteid);
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
                    Console.WriteLine("Duplicate Athlete: "+athlete.Fname);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static AthleteEntity GetAthleteByACNum(String ACNum)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VAIO;Initial catalog=AO_TESTDB_V7;Integrated Security=True");
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
                    athlete.Fname = proReader["Fname"].ToString();
                    athlete.Lname = proReader["Lname"].ToString();
                    athlete.DOB = Convert.ToDateTime(proReader["DOB"]);
                    athlete.Gender = proReader["Gender"].ToString();
                    athlete.ClubCode = proReader["ClubCode"].ToString();
                    athlete.Address = proReader["Address"].ToString();
                    athlete.City = proReader["City"].ToString();
                    athlete.Phone = proReader["Phone"].ToString();
                    athlete.Email = proReader["Email"].ToString();
                    athlete.HeadShot = proReader["HeadShot"].ToString();
                    athlete.AthleteSpecialNoteid = Convert.ToInt32(proReader["AthleteSpecialNoteid"]);
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
                Console.WriteLine(ex.Message);
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
            SqlConnection connection = new SqlConnection(@"Data Source=VAIO;Initial catalog=AO_TESTDB_V7;Integrated Security=True");
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
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        public static int GetAthleteIdByName(String fName, String lName, DateTime dob)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VAIO;Initial catalog=AO_TESTDB_V7;Integrated Security=True");
            string selectStatement
                = "SELECT * "
                + "FROM AThletes "
                + "WHERE Fname+Lname+CONVERT(VARCHAR(10),DOB,105) = @Fname+@Lname+@DOB";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@Fname", fName);
            selectCommand.Parameters.AddWithValue(
               "@Lname", lName);
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
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
