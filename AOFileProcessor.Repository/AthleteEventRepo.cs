using System;
using System.Collections.Generic;
using System.Text;
using AOFileProcessor.Entities;
using System.Data.SqlClient;

namespace AOFileProcessor.Repository
{
    public class AthleteEventRepo
    {
        public static int AddAthleteEvent(AthleteEventEntity athleteEvent)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VAIO;Initial catalog=AO_TESTDB_V7;Integrated Security=True");
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
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int GetAthleteEventId(String eventName,String eventRound)
        {
            
            SqlConnection connection = new SqlConnection(@"Data Source=VAIO;Initial catalog=AO_TESTDB_V7;Integrated Security=True");
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
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
