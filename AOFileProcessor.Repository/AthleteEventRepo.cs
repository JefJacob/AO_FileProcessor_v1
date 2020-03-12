using System;
using System.Collections.Generic;
using System.Text;
using AOFileProcessor.Entities;
using System.Data.SqlClient;
using System.Configuration;
using NLog;

namespace AOFileProcessor.Repository
{
    public class AthleteEventRepo
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static int AddAthleteEvent(AthleteEventEntity athleteEvent)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string insertStatement =
                "INSERT into AthleteEvents " +
                "(Name,	Gender,	Division,	EventRound) " +
                "VALUES (@Name,	@Gender,	@Division,	@EventRound)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);

            //insertCommand.Parameters.AddWithValue("@EventId", athleteEvent.EventId);
            insertCommand.Parameters.AddWithValue("@Name", athleteEvent.Name);
            insertCommand.Parameters.AddWithValue("@Gender", athleteEvent.Gender);
            insertCommand.Parameters.AddWithValue("@Division", athleteEvent.Division);
            insertCommand.Parameters.AddWithValue("@EventRound", athleteEvent.EventRound);


            try
            {
                connection.Open();
                int value = insertCommand.ExecuteNonQuery();
                return value;

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

        public static int GetAthleteEventId(String eventName,String eventRound)
        {
            
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string selectStatement
                = "SELECT * "
                + "FROM AThleteEvents "
                + "WHERE Gender+' '+Name+' '+Division+' '+EventRound =@EventName+' '+@EventRound";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@EventName", eventName);
            selectCommand.Parameters.AddWithValue(
               "@EventRound", eventRound);
            try
            {
                connection.Open();
                SqlDataReader proReader =
                    selectCommand.ExecuteReader(
                        System.Data.CommandBehavior.SingleRow);
                if (proReader.Read())
                {
                    int AthleteEventId = Convert.ToInt32(proReader["EventId"]);
                    return AthleteEventId;
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

        public static string GetAthleteEventName(int eventid)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
            string selectStatement
                = "SELECT * "
                + "FROM AThleteEvents "
                + "WHERE EventId=@EventId";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@EventId", eventid);
            
            try
            {
                connection.Open();
                SqlDataReader proReader =
                    selectCommand.ExecuteReader(
                        System.Data.CommandBehavior.SingleRow);
                if (proReader.Read())
                {
                    string AthleteEventName = proReader["Gender"].ToString()+" "+ proReader["Name"].ToString() + " "+proReader["Division"].ToString() + " "+proReader["EventRound"].ToString();
                    return AthleteEventName;
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
