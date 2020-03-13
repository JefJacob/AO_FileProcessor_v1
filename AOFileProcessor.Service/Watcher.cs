using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using AOFileProcessor;
using System.Configuration;

namespace AOFileProcessor.Service
{
    public static class Watcher
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static void MonitorDirectory(string path)
        {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = path;

            fileSystemWatcher.Created += FileSystemWatcher_Created;

            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;

            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)

        {
            //the Access file spins up a .ldb file when its being processed. This invokes FileSystemWatcher_Created again causing eth system to crash.
            //The below condition avoids this scenario 
            if (e.Name != null && e.Name.Contains(".mdb"))
            {
                logger.Info("File created: {0}", e.FullPath);

                logger.Info("Application Started");
                DataReader.ProcessFile(e.FullPath);
                var destinationFilePath = ConfigurationManager.AppSettings["dest"];
                var destinationFile = destinationFilePath + Path.GetFileName(e.FullPath);
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                    logger.Info("Duplicate File Deleted in Backup path");
                }


                File.Move(e.FullPath, destinationFile);
                logger.Info("File Moved  " + e.FullPath + "->" + destinationFile);
                logger.Info("Application Exited Successfully");
            }
        }

        private static void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)

        {

            logger.Info("File renamed: {0}", e.Name); ;

        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)

        {

            logger.Info("File deleted: {0}", e.Name);

        }
    }
}
