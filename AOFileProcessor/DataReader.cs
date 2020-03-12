using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Odbc;
using AOFileProcessor.Entities;
using AOFileProcessor.Repository;
using AOFileProcessor.Logic;
using NLog;
using System.Configuration;

namespace AOFileProcessor
{
    public class DataReader
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static string[] GetInputPaths()
        {
            //List<string> paths;
            var dataPath = ConfigurationManager.AppSettings["datapath"];
            string[] subDirectory =
                Directory.GetDirectories(dataPath, "*",
                                   searchOption: SearchOption.TopDirectoryOnly);
            return subDirectory;
        }
        public static void ProcessFile(string folderPath)
        {
            
            var file = Directory.GetFiles(@folderPath, "*.mdb").FirstOrDefault();
            if (File.Exists(file))
            {
               
                OdbcConnectionStringBuilder builder =
            new OdbcConnectionStringBuilder();
                builder.Driver = "Microsoft Access Driver (*.mdb)";
                builder.Add("DBQ",  file);
                Console.WriteLine(builder.ConnectionString);
                string fileName = Path.GetFileName(file);
                try
                {
                    if (DataTransfer.GetCompId(fileName) != 0)
                        ReadData(builder.ConnectionString, fileName);
                    else
                    {
                        Console.WriteLine("Competition does not exist. Please create new Competition");
                        logger.Error("Competition does not exist. Please create new Competition");
                    }

                }
                catch (Exception ex)
                {
                    logger.Error("Exception : " + ex.Message);
                }
            }
        }
        public static void ReadData(string connectionString,string fileName)
        {
            logger.Info("Application has started Reading Access DB");
            //Club data 
            string queryStringClub = "SELECT * FROM Team";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryStringClub, connection);
                connection.Open();
                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                DataTransfer.ProcessClubData(reader);
                // Call Close when done reading.
                reader.Close();
            }

            //Standard events
            string queryStringResults = "select Full_Eventname,Rnd_ltr,Results.First_name,Results.Last_name,Results.Team_Abbr,Results.Reg_no,Athlete.Birth_date,Athlete.Ath_Sex, Res_markDisplay,Res_wind,Res_place from Results inner join Athlete on Athlete.Ath_no=Results.Ath_no where Full_Eventname not like '%relay%' and Event_name not like '%pentathlon%' and  Full_Eventname not like '%masters%'";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryStringResults, connection);
                connection.Open();
                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                DataTransfer.ProcessResultsData(reader, fileName);
                reader.Close();
            }

            //Relay events and sprint Medley
            string queryStringRelayResults = "select Full_Eventname,Relay_Ltr,  a.First_name,a.Last_name,  r.Team_Abbr,Res_markDisplay,Res_wind,Res_place  ,a.Birth_date,a.Reg_no,a.Ath_sex from Results r inner join Athlete a ON a.Ath_no = r.RelayLeg1_Ath_no OR a.Ath_no = r.RelayLeg2_Ath_no OR a.Ath_no = r.RelayLeg3_Ath_no OR a.Ath_no = r.RelayLeg4_Ath_no where Full_Eventname like '%relay%'  OR Full_Eventname like '%Sprint Medley%'";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryStringRelayResults, connection);
                connection.Open();
                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                DataTransfer.ProcessResultsDataRelay(reader, fileName);
                // Call Close when done reading.
                reader.Close();
            }

            //combined events
            string queryStringCombinedResults = "select Event_name,MultiSubEvent_name, Rnd_ltr,Results.First_name,Results.Last_name,Results.Team_Abbr,Athlete.Reg_no, Athlete.Birth_date,Athlete.Ath_Sex, Res_markDisplay,Res_wind,Res_place,Event_score,Results.Event_dist,Divisions.Div_name from (Results inner join Athlete on Athlete.Ath_no = Results.Ath_no) inner join Divisions on Divisions.Div_no = Results.Div_no where Event_name like '%athlon%'  ";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryStringCombinedResults, connection);
                connection.Open();
                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                DataTransfer.ProcessResultsDataCombined(reader, fileName);
                // Call Close when done reading.
                reader.Close();
            }

            //Master events
            string queryStringMasterResults = "select Full_Eventname, Rnd_ltr,Results.First_name,Results.Last_name,Results.Team_Abbr,Athlete.Reg_no,Athlete.Birth_date,Athlete.Ath_Sex, Res_markDisplay,Res_wind,Res_place from Results inner join Athlete on Athlete.Ath_no=Results.Ath_no where Full_Eventname like '%masters%'  ";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryStringMasterResults, connection);
                connection.Open();
                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                DataTransfer.ProcessResultsDataMasters(reader, fileName);
                // Call Close when done reading.
                reader.Close();
            }

        }
    }
}
