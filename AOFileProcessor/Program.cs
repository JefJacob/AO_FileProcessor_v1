using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AOFileProcessor.Repository;
using NLog;

namespace AOFileProcessor
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Application Started");
            
            var subDirectory = DataReader.GetInputPaths();

            foreach (var dir in subDirectory)
            {
                DataReader.ProcessFile(dir);
            }


            logger.Info("Application Exited Successfully");
        }
    }
}
