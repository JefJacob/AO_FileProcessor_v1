using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AOFileProcessor.Repository;
using NLog;

namespace AOFileProcessor
{
    public class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            
            
            //var subDirectory = DataReader.GetInputPaths();

            //foreach (var dir in subDirectory)
            //{
            //    DataReader.ProcessFile(dir);
            //}


            
        }
        public static void AOProcessor(string path)
        {
            logger.Info("Application Started");
            DataReader.ProcessFile(path);
            var destinationFile = ConfigurationManager.AppSettings["dest"];
            destinationFile = destinationFile + Path.GetFileName(path);
            System.IO.File.Move(path, destinationFile);
            logger.Info("File Moved  "+ path+"->"+ destinationFile);
            logger.Info("Application Exited Successfully");
        }
    }
}
