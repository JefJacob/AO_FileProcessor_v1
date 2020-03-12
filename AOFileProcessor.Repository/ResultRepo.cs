using System;
using System.Collections.Generic;
using System.Text;
using AOFileProcessor.Entities;
using System.Data.SqlClient;
using NLog;
using System.Configuration;

namespace AOFileProcessor.Repository
{
    public class ResultRepo
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static int AddResult(ResultEntity result)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AODB"].ConnectionString);
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
                    logger.Error("Duplicate Result: " + result.CompId+"->"+result.EventId+"->"+result.AthleteId);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
