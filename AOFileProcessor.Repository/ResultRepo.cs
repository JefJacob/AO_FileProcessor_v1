using System;
using System.Collections.Generic;
using System.Text;
using AOFileProcessor.Entities;
using System.Data.SqlClient;

namespace AOFileProcessor.Repository
{
    public class ResultRepo
    {
        public static int AddResult(ResultEntity result)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VAIO;Initial catalog=AO_TESTDB_V7;Integrated Security=True");
            string insertStatement =
                "INSERT into Results " +
                "(CompId,	EventId,	AthleteId,	Mark,	Position,	Wind) " +
                "VALUES (@CompId,	@EventId,	@AthleteId,	@Mark,	@Position,	@Wind)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);

            insertCommand.Parameters.AddWithValue("@CompId", result.CompId);
            insertCommand.Parameters.AddWithValue("@EventId", result.EventId);
            insertCommand.Parameters.AddWithValue("@AthleteId", result.AthleteId);
            insertCommand.Parameters.AddWithValue("@Mark", result.Mark);
            insertCommand.Parameters.AddWithValue("@Position", result.Position);
            insertCommand.Parameters.AddWithValue("@Wind", result.Wind);



            try
            {
                connection.Open();
                int value = insertCommand.ExecuteNonQuery();
                return value;

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                    Console.WriteLine("Duplicate Result: " + result.CompId+"->"+result.EventId+"->"+result.AthleteId);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
