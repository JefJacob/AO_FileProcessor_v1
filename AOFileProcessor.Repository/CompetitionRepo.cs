using System;
using System.Collections.Generic;
using System.Text;
using AOFileProcessor.Entities;
using System.Data.SqlClient;

namespace AOFileProcessor.Repository
{
    public class CompetitionRepo
    {
        public static int GetCompetitionId(String fileName)
        {

            SqlConnection connection = new SqlConnection(@"Data Source=VAIO;Initial catalog=AO_TESTDB_V7;Integrated Security=True");
            string selectStatement
                = "SELECT * "
                + "FROM Competitions "
                + "WHERE Name=@Name";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue(
                "@Name", fileName.Replace(".mdb",""));
          
            try
            {
                connection.Open();
                SqlDataReader proReader =
                    selectCommand.ExecuteReader(
                        System.Data.CommandBehavior.SingleRow);
                if (proReader.Read())
                {
                    int CompId = Convert.ToInt32(proReader["CompId"]);
                    return CompId;
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
